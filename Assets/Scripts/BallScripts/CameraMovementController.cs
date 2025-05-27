using Unity.VisualScripting;
using UnityEngine;

public class TopDownFollow : MonoBehaviour
{
    public Transform m_Target;              // El objeto que la cámara seguirá (por ejemplo, la bola)
    public Vector2 m_FollowAmount = new Vector2(2f, 2f); // Cuánto se puede mover la cámara desde su posición base
    public float m_SmoothSpeed = 0.1f;      // Qué tan rápido se mueve la cámara hacia la nueva posición

    public Vector3 m_FinalPosition;
    public Quaternion m_FinalRotation;
    private Vector3 m_InitialPosition;
    private Quaternion m_InitialRotation;

    public float m_TimeToArrive;
    private float m_Time;

    public float m_LinearSpeed;
    public float m_AngularSpeed;

    public bool m_CameraArrivedDesiredRotation;
    public bool m_CameraArrivedDesiredPosition;

    public bool m_GamePlayCamera;
    public GameObject m_Ball;

    private Vector3 m_InitialPositionForGameplay;      // Posición inicial de la cámara

    void Start()
    {
        transform.position = GameManager.m_PositionOfCamera;
        transform.rotation = GameManager.m_RotationOfCamera;

        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if(!m_GamePlayCamera)
        {
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
            CheckDesiredPosition();
        }
        else
        {
            if (m_Target == null) return;

            // Calcula el desplazamiento horizontal (x, z) en relación al objetivo
            Vector3 offset = new Vector3(
                Mathf.Clamp(m_Target.position.x - m_InitialPositionForGameplay.x, -m_FollowAmount.x, m_FollowAmount.x),
                0,
                Mathf.Clamp(m_Target.position.z - m_InitialPositionForGameplay.z, -m_FollowAmount.y, m_FollowAmount.y)
            );

            // Aplica suavizado al movimiento de la cámara
            Vector3 desiredPosition = m_InitialPositionForGameplay + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, m_SmoothSpeed * Time.deltaTime);
        }
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

    private void CheckDesiredPosition()
    {
        if (m_CameraArrivedDesiredRotation && m_CameraArrivedDesiredPosition)
        {
            m_GamePlayCamera = true;
            m_Ball.SetActive(true);

            m_InitialPositionForGameplay = transform.position;
        }
    }
}
