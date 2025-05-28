using UnityEngine;

public class BallDetector : MonoBehaviour
{

    public Beat m_Beat;

    private Collider m_Collider;

    public AudioManager m_AudioManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Beat = GetComponentInChildren<Beat>();
        m_Collider = GetComponent<Collider>();
    }

    public float m_ExpulsionForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.m_GhostBall)
        {
            return;
        }
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null && other.tag == "BALL")
        {
            m_Beat.m_ActivateExpansion = true;

            m_AudioManager.PlaySFX(m_AudioManager.m_BumperSound);

            Vector3 direccionExpulsion = (other.transform.position - transform.position).normalized;
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(direccionExpulsion * m_ExpulsionForce, ForceMode.Impulse);
        }
    }


}
