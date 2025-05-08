using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private BoxCollider m_BoxCollider;
    public Transform m_RespawnPoint;
    public GameObject particles;
    public GameObject Goalparticles;

    private bool m_ParticlesSpawn;

    private GameObject m_Ball;

    private bool m_Goal;

    private float m_TimeToRespawn;
    private float m_RemainingTimeToRespawn;

    public Material m_MaterialPlayer;

    void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
        m_Goal = false;
        m_TimeToRespawn = 2.5f;
        m_RemainingTimeToRespawn = m_TimeToRespawn;
        m_ParticlesSpawn = false;
    }

    private void Update()
    {
        if(m_Goal)
        {
            if(m_RemainingTimeToRespawn <= 0.0f)
            {
                m_Ball.SetActive(true);
                m_Ball.GetComponent<MeshRenderer>().material = m_MaterialPlayer;
                Rigidbody rb = m_Ball.GetComponent<Rigidbody>();
                rb.Sleep();
                m_Ball.transform.position = m_RespawnPoint.position;
                Vector3 direction = (transform.position - m_RespawnPoint.position).normalized;
                rb.AddForce(direction * 2, ForceMode.Impulse);
                m_RemainingTimeToRespawn = m_TimeToRespawn;
                m_Goal = false;
            }
            else if (m_ParticlesSpawn && m_RemainingTimeToRespawn <= 0.5f)
            {
                if (particles != null)
                {
                    Instantiate(particles, m_RespawnPoint.position, Quaternion.identity);
                    m_ParticlesSpawn = false;
                }
            }
            else
            {
                m_RemainingTimeToRespawn -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BALL")
        {
            if (Goalparticles != null)
            {
                Instantiate(Goalparticles, other.transform.position, Quaternion.identity);
            }
            m_Ball = other.gameObject;
            m_Ball.SetActive(false);
            m_Goal = true;
            m_ParticlesSpawn = true;

            if(tag.Equals("Player1"))
            {
                GameManager.m_PlayerOwner = false;
            }
            if (tag.Equals("Player2"))
            {
                GameManager.m_PlayerOwner = true;
            }
        }
    
    }
}
