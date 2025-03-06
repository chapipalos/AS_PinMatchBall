using UnityEngine;

public class GravityController : MonoBehaviour
{
    public GameObject m_Ball;

    public Transform m_PointOfGravity1;
    public Transform m_PointOfGravity2;

    private Rigidbody m_BallRigidbody;

    private void Awake()
    {
        m_Ball = GameObject.FindGameObjectWithTag("BALL");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (m_Ball != null)
        {
            m_BallRigidbody = m_Ball.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_Ball != null)
        {
            if(m_Ball.transform.position.z >= 0)
            {
                m_BallRigidbody.AddForce((m_Ball.transform.position - m_PointOfGravity2.position).normalized * 0.1f * Vector3.Distance(m_Ball.transform.position, m_PointOfGravity2.position), ForceMode.Force);
            }
            else
            {
                m_BallRigidbody.AddForce((m_Ball.transform.position - m_PointOfGravity1.position).normalized * 0.1f * Vector3.Distance(m_Ball.transform.position, m_PointOfGravity1.position), ForceMode.Force);
            }
        }
    }
}
