using Unity.VisualScripting;
using UnityEngine;

public class GoalRespawnController : MonoBehaviour
{
    private BoxCollider m_BoxCollider;
    public Transform m_RespawnPoint;

    public GameObject m_BeamAtSpawnPrefab;
    private GameObject m_BeamAtSpawnEffect;
    private ParticleSystem[] m_BeamAtSpawnParticleSystem;

    public GameObject m_GoalExplosionPrefab;
    private GameObject m_GoalExplosionEffect;
    private ParticleSystem[] m_GoalExplosionParticleSystem;

    private bool m_ParticlesSpawn;

    private GameObject m_Ball;

    private bool m_Goal;

    private float m_TimeToRespawn;
    private float m_RemainingTimeToRespawn;

    public Material m_MaterialPlayer;

    public ScoreManager m_ScoreManager;
    public AudioManager m_AudioManager;

    private void Awake()
    {
        m_BeamAtSpawnEffect = GameObject.Instantiate(m_BeamAtSpawnPrefab);
        m_BeamAtSpawnEffect.SetActive(false);
        m_BeamAtSpawnParticleSystem = m_BeamAtSpawnEffect.GetComponentsInChildren<ParticleSystem>();

        m_GoalExplosionEffect = GameObject.Instantiate(m_GoalExplosionPrefab);
        m_GoalExplosionParticleSystem = m_GoalExplosionEffect.GetComponentsInChildren<ParticleSystem>();
    }

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
                if (m_BeamAtSpawnPrefab != null)
                {
                    m_BeamAtSpawnEffect.SetActive(true);
                    foreach (ParticleSystem ps in m_BeamAtSpawnParticleSystem)
                    {
                        if (ps.CompareTag("Beam"))
                        {
                            ps.transform.position = m_RespawnPoint.position - new Vector3(0f, 13f, 0f);
                        }
                        else
                        {
                            ps.transform.position = m_RespawnPoint.position;
                        }
                        m_AudioManager.PlaySFX(m_AudioManager.m_RespawnSound);

                        ps.Play();

                    }
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
         

            foreach (ParticleSystem ps in m_GoalExplosionParticleSystem)
            {
                ps.transform.position = other.transform.position;
                ps.Play();
                m_AudioManager.PlaySFX(m_AudioManager.m_GoalSound);

            }




            m_ScoreManager.IncreaseScore();

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

            GameManager.m_GhostBall = false;
            GameManager.m_RedFrozenPowerUp = false;
            GameManager.m_BlueFrozenPowerUp = false;
            GameManager.m_RedRobotSearching = false;
            GameManager.m_BlueRobotSearching = false;
            GameManager.m_RedStunnedPowerUp = false;
            GameManager.m_BlueStunnedPowerUp = false;
            GameManager.m_RedStunnedPowerUpActive = false;
            GameManager.m_BlueStunnedPowerUpActive = false;
            GameManager.m_RedSplash = false;
            GameManager.m_BlueSplash = false;
        }
    
    }
}
