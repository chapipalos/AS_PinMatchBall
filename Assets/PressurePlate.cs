using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Vector3 m_InitialPosition;
    public Vector3 m_FinalPosition;

    public float m_PlateSpeed = 5f;
    public float m_PlateCooldown = 20f;
    private float m_PlateCooldownRemaining = 0f;

    public FanController m_FanController;

    public bool m_IsPressed = false;
    public bool m_IsReturning = false;

    public bool m_FanType;

    void Start()
    {
        m_InitialPosition = transform.position;
    }

    void Update()
    {
        if (m_IsPressed)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_FinalPosition, m_PlateSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_FinalPosition) < 0.01f)
            {
                m_PlateCooldownRemaining -= Time.deltaTime;

                if (m_PlateCooldownRemaining <= 0f)
                {
                    m_IsPressed = false;
                    m_IsReturning = true;
                }
            }
        }
        else if (m_IsReturning)
        {
            // Mover hacia arriba
            transform.position = Vector3.MoveTowards(transform.position, m_InitialPosition, m_PlateSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_InitialPosition) < 0.01f)
            {
                m_IsReturning = false;
                m_PlateCooldownRemaining = m_PlateCooldown;

                if (!m_FanType)
                {
                    GameManager.m_UpperFanActive = false;
                }
                else if (m_FanType)
                {
                    GameManager.m_BottomFanActive = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.m_GhostBall) return;

        if (other.CompareTag("BALL") && !m_IsPressed && !m_IsReturning)
        {
            m_IsPressed = true;
            m_PlateCooldownRemaining = m_PlateCooldown;

            if (!m_FanType)
            {
                GameManager.m_UpperFanActive = true;
            }
            else if (m_FanType)
            {
                GameManager.m_BottomFanActive = true;
            }
            
        }
    }
}
