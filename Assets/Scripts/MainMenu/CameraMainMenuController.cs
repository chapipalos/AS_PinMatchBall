using UnityEngine;

public class CameraMainMenuController : MonoBehaviour
{
    public Transform m_FinalPosition;
    private Vector3 m_InitialPosition;
    private Quaternion m_InitialRotation;

    public float m_TimeToArrive;
    private float m_Time;

    public float m_LinearSpeed;
    public float m_AngularSpeed;

    public bool m_CameraArrivedDesiredRotation;
    public bool m_CameraArrivedDesiredPosition;

    public GameObject m_Canvas;
    public GameObject m_Screen;
    public Material m_ScreenOnMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        float factor = m_Time / m_TimeToArrive;
        Movement(factor);
        Rotate(factor);
        if(transform.position == m_FinalPosition.position)
        {
            m_CameraArrivedDesiredPosition = true;
        }
        if (transform.rotation == m_FinalPosition.rotation)
        {
            m_CameraArrivedDesiredRotation = true;
        }
        ActivateCanvas();
    }
    
    private void Movement(float dt)
    {
        if (m_CameraArrivedDesiredPosition)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(m_InitialPosition, m_FinalPosition.position, m_LinearSpeed * dt);
    }

    private void Rotate(float dt)
    {
        if (m_CameraArrivedDesiredRotation)
        {
            return;
        }
        transform.rotation = Quaternion.Slerp(m_InitialRotation, m_FinalPosition.rotation, m_AngularSpeed * dt);
    }

    private void ActivateCanvas()
    {
        if (m_CameraArrivedDesiredRotation && m_CameraArrivedDesiredPosition)
        {
            m_Canvas.SetActive(true);
            m_Screen.GetComponent<MeshRenderer>().material = m_ScreenOnMaterial;
        }
    }
}
