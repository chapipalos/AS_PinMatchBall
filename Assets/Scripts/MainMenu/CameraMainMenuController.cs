using UnityEngine;

public class CameraMainMenuController : MonoBehaviour
{
    public Vector3 m_FinalPosition;
    public Quaternion m_FinalRotation;
    private Vector3 m_InitialPosition;
    private Quaternion m_InitialRotation;

    public float m_TimeToArrive;
    public float m_Time;

    public float m_LinearSpeed;
    public float m_AngularSpeed;

    public bool m_CameraArrivedDesiredRotation;
    public bool m_CameraArrivedDesiredPosition;

    public GameObject m_Canvas;
    public GameObject m_Screen;
    public Material m_ScreenOnMaterial;
    public Material m_ScreenOffMaterial;

    private bool m_InitMovement = false;
    public float m_TimeToStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = GameManager.m_PositionOfCamera;
        transform.rotation = GameManager.m_RotationOfCamera;
        m_Screen.GetComponent<MeshRenderer>().material = m_ScreenOffMaterial;
        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;
        Invoke("InitializeMovement", m_TimeToStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_InitMovement)
        {
            return;
        }
        m_Time += Time.deltaTime;
        float factor = m_Time / m_TimeToArrive;
        Movement(factor);
        Rotate(factor);
        if (transform.position == m_FinalPosition)
        {
            m_CameraArrivedDesiredPosition = true;
        }
        if (transform.rotation == m_FinalRotation)
        {
            m_CameraArrivedDesiredRotation = true;
        }
        ActivateCanvas();
    }

    private void InitializeMovement()
    {
        m_InitMovement = true;
    }

    private void Movement(float dt)
    {
        if (m_CameraArrivedDesiredPosition)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(m_InitialPosition, m_FinalPosition, m_LinearSpeed * dt);
    }

    private void Rotate(float dt)
    {
        if (m_CameraArrivedDesiredRotation)
        {
            return;
        }
        transform.rotation = Quaternion.Slerp(m_InitialRotation, m_FinalRotation, m_AngularSpeed * dt);
    }

    private void ActivateCanvas()
    {
        if (m_CameraArrivedDesiredRotation && m_CameraArrivedDesiredPosition)
        {
            m_Canvas.SetActive(true);
            m_Screen.GetComponent<MeshRenderer>().material = m_ScreenOnMaterial;
            GameManager.m_PositionOfCamera = transform.position;
            GameManager.m_RotationOfCamera = transform.rotation;
            m_CameraArrivedDesiredPosition = false;
            m_CameraArrivedDesiredRotation = false;
        }
    }
}
