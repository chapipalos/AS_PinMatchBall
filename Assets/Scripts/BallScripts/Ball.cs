using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Material m_MaterialPlayer1;
    public Material m_MaterialPlayer2;

    private MeshRenderer m_MeshRenderer;
    private MeshFilter m_MeshFilter;

    public GameObject m_SparklePrefab;
    private GameObject m_SparkleEffect;
    private ParticleSystem[] m_SparklePartycleSystem;

    private Mesh m_NormalMesh;
    public Mesh m_GhostMesh;
    public Mesh m_SpikeMesh;

    private PowerUpsPoolManager m_PowerUpsPoolManager;

    private void Awake()
    {
        m_SparkleEffect = GameObject.Instantiate(m_SparklePrefab);
        m_SparklePartycleSystem = m_SparkleEffect.GetComponentsInChildren<ParticleSystem>();
        m_PowerUpsPoolManager = FindFirstObjectByType<PowerUpsPoolManager>();
    }

    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_MeshFilter = GetComponent<MeshFilter>();
        m_NormalMesh = m_MeshFilter.mesh;
    }

    private void Update()
    {
        if (GameManager.m_GhostBall)
        {
            m_MeshFilter.mesh = m_GhostMesh;
        }
        else if (GameManager.m_RedStunnedPowerUp || GameManager.m_BlueStunnedPowerUp)
        {
            m_MeshFilter.mesh = m_SpikeMesh;
        }
        else
        {
            m_MeshFilter.mesh = m_NormalMesh;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            foreach (ParticleSystem ps in m_SparklePartycleSystem)
            {
                ps.transform.position = transform.position;
                ps.Play();
            }
        }
        if (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.CompareTag("Player1"))
        {
            if (!GameManager.m_RedFrozenPowerUp && !GameManager.m_BlueFrozenPowerUp)
            {
                m_MeshRenderer.material = m_MaterialPlayer1;
                GameManager.m_PlayerOwner = false;
            }
        }
        if (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.CompareTag("Player2"))
        {
            if (!GameManager.m_RedFrozenPowerUp && !GameManager.m_BlueFrozenPowerUp)
            {
                m_MeshRenderer.material = m_MaterialPlayer2;
                GameManager.m_PlayerOwner = true;
            }
        }
        if (collision.gameObject.tag.Contains("Left") && GameManager.m_RedStunnedPowerUp)
        {
            GameManager.m_RedStunnedPowerUpActive = true;
            GameManager.m_StunnedSide = false;
        }
        if (collision.gameObject.tag.Contains("Left") && GameManager.m_BlueStunnedPowerUp)
        {
            GameManager.m_BlueStunnedPowerUpActive = true;
            GameManager.m_StunnedSide = false;
        }
        if (collision.gameObject.tag.Contains("Right") && GameManager.m_RedStunnedPowerUp)
        {
            GameManager.m_RedStunnedPowerUpActive = true;
            GameManager.m_StunnedSide = true;
        }
        if (collision.gameObject.tag.Contains("Right") && GameManager.m_BlueStunnedPowerUp)
        {
            GameManager.m_BlueStunnedPowerUpActive = true;
            GameManager.m_StunnedSide = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FreezePU"))
        {
            if (GameManager.m_PlayerOwner)
            {
                GameManager.m_RedFrozenPowerUp = true;
            }
            else
            {
                GameManager.m_BlueFrozenPowerUp = true;
            }
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
        if (other.gameObject.CompareTag("SpikePU"))
        {
            if (GameManager.m_PlayerOwner)
            {
                GameManager.m_RedStunnedPowerUp = true;
            }
            else
            {
                GameManager.m_BlueStunnedPowerUp = true;
            }
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
        if (other.gameObject.CompareTag("GhostPU"))
        {
            GameManager.m_GhostBall = true;
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
        if (other.gameObject.CompareTag("ShieldPU"))
        {
            if(GameManager.m_PlayerOwner)
            {
                GameManager.m_BlueRobot = true;
            }
            else
            {
                GameManager.m_RedRobot = true;
            }
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
        if (other.gameObject.CompareTag("SplashPU"))
        {
            if (GameManager.m_PlayerOwner)
            {
                GameManager.m_RedSplash = true;
            }
            else
            {
                GameManager.m_BlueSplash = true;
            }
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
    }
}
