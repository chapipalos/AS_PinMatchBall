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

    public GameObject m_ZapPrefab;
    private GameObject m_ZapEffect;
    private ParticleSystem[] m_ZapPartycleSystem;

    private Mesh m_NormalMesh;
    public MeshRenderer m_GhostObject;
    public Mesh m_SpikeMesh;

    private PowerUpsPoolManager m_PowerUpsPoolManager;

    public AudioManager m_AudioManager;


    private void Awake()
    {
        m_SparkleEffect = GameObject.Instantiate(m_SparklePrefab);
        m_SparklePartycleSystem = m_SparkleEffect.GetComponentsInChildren<ParticleSystem>();

        m_ZapEffect = GameObject.Instantiate(m_ZapPrefab);
        m_ZapPartycleSystem = m_ZapEffect.GetComponentsInChildren<ParticleSystem>();

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
            m_GhostObject.enabled = true;
            m_MeshRenderer.enabled = false;
        }
        else if (GameManager.m_RedStunnedPowerUp || GameManager.m_BlueStunnedPowerUp)
        {
            m_MeshFilter.mesh = m_SpikeMesh;

            m_GhostObject.enabled = false;
            m_MeshRenderer.enabled = true;
        }
        else
        {
            m_GhostObject.enabled = false;
            m_MeshRenderer.enabled = true;

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
                m_AudioManager.PlaySFX(m_AudioManager.m_BallWallBounceSound);
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
            if(GameManager.m_PlayerOwner)
            {
                return;
            }
            GameManager.m_RedStunnedPowerUpActive = true;
            foreach (ParticleSystem ps in m_ZapPartycleSystem)
            {
                ps.transform.position = collision.transform.position;
                ps.Play();
                m_AudioManager.PlaySFX(m_AudioManager.m_StunSound);
            }
            GameManager.m_StunnedSide = false;
        }
        if (collision.gameObject.tag.Contains("Left") && GameManager.m_BlueStunnedPowerUp)
        {
            if (!GameManager.m_PlayerOwner)
            {
                return;
            }
            GameManager.m_BlueStunnedPowerUpActive = true;
            foreach (ParticleSystem ps in m_ZapPartycleSystem)
            {
                ps.transform.position = collision.transform.position;
                ps.Play();
                m_AudioManager.PlaySFX(m_AudioManager.m_StunSound);
            }
            GameManager.m_StunnedSide = false;
        }
        if (collision.gameObject.tag.Contains("Right") && GameManager.m_RedStunnedPowerUp)
        {
            if (GameManager.m_PlayerOwner)
            {
                return;
            }
            GameManager.m_RedStunnedPowerUpActive = true;
            foreach (ParticleSystem ps in m_ZapPartycleSystem)
            {
                ps.transform.position = collision.transform.position;
                ps.Play();
                m_AudioManager.PlaySFX(m_AudioManager.m_StunSound);
            }
            GameManager.m_StunnedSide = true;
        }
        if (collision.gameObject.tag.Contains("Right") && GameManager.m_BlueStunnedPowerUp)
        {
            if (!GameManager.m_PlayerOwner)
            {
                return;
            }
            GameManager.m_BlueStunnedPowerUpActive = true;
            foreach (ParticleSystem ps in m_ZapPartycleSystem)
            {
                ps.transform.position = collision.transform.position;
                ps.Play();
                m_AudioManager.PlaySFX(m_AudioManager.m_StunSound);
            }
            GameManager.m_StunnedSide = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FreezePU"))
        {
            if (GameManager.m_PlayerOwner)
            {
                GameManager.m_RedFrozenPowerUp = false;
                GameManager.m_RedFrozenPowerUp = true;
                m_AudioManager.PlaySFX(m_AudioManager.m_FreezePowerUpSound);
            }
            else
            {
                GameManager.m_BlueFrozenPowerUp = false;
                GameManager.m_BlueFrozenPowerUp = true;
                m_AudioManager.PlaySFX(m_AudioManager.m_FreezePowerUpSound);
            }
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
        if (other.gameObject.CompareTag("SpikePU"))
        {
            if (GameManager.m_PlayerOwner)
            {
                GameManager.m_RedStunnedPowerUp = false;
                GameManager.m_RedStunnedPowerUp = true;
                m_AudioManager.PlaySFX(m_AudioManager.m_SpikeBallPowerUpSound);
            }
            else
            {
                GameManager.m_BlueStunnedPowerUp = false;
                GameManager.m_BlueStunnedPowerUp = true;
                m_AudioManager.PlaySFX(m_AudioManager.m_SpikeBallPowerUpSound);
            }
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
        if (other.gameObject.CompareTag("GhostPU"))
        {
            GameManager.m_GhostBall = false;
            GameManager.m_GhostBall = true;
            m_AudioManager.PlaySFX(m_AudioManager.m_GhostBallPowerUpSound);
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
        if (other.gameObject.CompareTag("ShieldPU"))
        {
            if(GameManager.m_PlayerOwner)
            {
                GameManager.m_BlueRobot = true;
                m_AudioManager.PlaySFX(m_AudioManager.m_ShieldPowerUpSound);
            }
            else
            {
                GameManager.m_RedRobot = true;
                m_AudioManager.PlaySFX(m_AudioManager.m_ShieldPowerUpSound);
            }
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
        if (other.gameObject.CompareTag("SplashPU"))
        {
            if (GameManager.m_PlayerOwner)
            {
                GameManager.m_RedSplash = false;
                GameManager.m_RedSplash = true;
                m_AudioManager.PlaySFX(m_AudioManager.m_BlooperPowerUpSound);
            }
            else
            {
                GameManager.m_BlueSplash = false;
                GameManager.m_BlueSplash = true;
                m_AudioManager.PlaySFX(m_AudioManager.m_BlooperPowerUpSound);
            }
            m_PowerUpsPoolManager.Return(other.gameObject);
        }
    }
}
