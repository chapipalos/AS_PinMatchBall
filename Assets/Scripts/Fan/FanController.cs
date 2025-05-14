using UnityEngine;

public class FanController : MonoBehaviour
{
    public float rotationSpeed = 90f; // Degrees per second
    private float m_CurrentSpeed;

    public float m_FanForce;

    public Transform m_Blades;

    public GameObject m_WindPrefab;
    private GameObject m_WindEffect;
    private ParticleSystem m_WindParticleSystem;

    private bool m_Effect;

    private void Awake()
    {
        m_WindEffect = Instantiate(m_WindPrefab);
        m_WindEffect.transform.position = m_Blades.transform.position;
        m_WindParticleSystem = m_WindEffect.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        m_CurrentSpeed = 0f;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        if (GameManager.m_FanRotating)
        {
            m_CurrentSpeed += rotationSpeed * dt;
            m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, 0f, rotationSpeed);
            m_Blades.Rotate(Vector3.forward, m_CurrentSpeed);
            Debug.Log("Rotating Fan");
            if(!m_Effect)
            {
                m_WindParticleSystem.Play();
                m_Effect = true;
            }
        }
        else
        {
            m_CurrentSpeed -= rotationSpeed * dt;
            m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, 0f, rotationSpeed);
            m_Blades.Rotate(Vector3.forward, m_CurrentSpeed);
            Debug.Log("No Rotating Fan");
            if (m_Effect)
            {
                m_WindParticleSystem.Stop();
                m_Effect = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameManager.m_FanRotating = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.m_FanRotating = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.m_FanRotating && other.CompareTag("BALL"))
        {
            float distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.right * (1/distance) * m_FanForce, ForceMode.Impulse);
        }
    }
}
