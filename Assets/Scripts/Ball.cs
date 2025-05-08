using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Material m_MaterialPlayer1;
    public Material m_MaterialPlayer2;

    private MeshRenderer m_MeshRenderer;

    public GameObject m_SparklePrefab;
    private GameObject m_SparkleEffect;
    private ParticleSystem[] m_SparklePartycleSystem;

    private void Awake()
    {
        m_SparkleEffect = GameObject.Instantiate(m_SparklePrefab);
        m_SparklePartycleSystem = m_SparkleEffect.GetComponentsInChildren<ParticleSystem>();
    }

    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player1"))
        {
            m_MeshRenderer.material = m_MaterialPlayer1;
            GameManager.m_PlayerOwner = false;
        }
        if(collision.gameObject.CompareTag("Player2"))
        {
            m_MeshRenderer.material = m_MaterialPlayer2;
            GameManager.m_PlayerOwner = true;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            foreach (ParticleSystem ps in m_SparklePartycleSystem)
            {
                ps.transform.position = transform.position;
                ps.Play();
            }
        }
    }
}
