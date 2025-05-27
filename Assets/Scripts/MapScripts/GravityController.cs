using UnityEngine;

public class GravityController : MonoBehaviour
{
    public GameObject m_Ball;

    public Transform m_PointOfGravity1;
    public Transform m_PointOfGravity2;
    public float velocityGravity=0.01f;
    private Rigidbody m_BallRigidbody;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_Ball != null && !m_Ball.activeSelf)
        {
            m_BallRigidbody = m_Ball.GetComponent<Rigidbody>();
        }
        else 
        {
            if(m_Ball.transform.position.z >= 0)
            {
                m_BallRigidbody.AddForce(Vector3.forward * velocityGravity * Vector3.Distance(m_Ball.transform.position, m_PointOfGravity2.position), ForceMode.Force);
            }
            else
            {
                m_BallRigidbody.AddForce(-Vector3.forward * velocityGravity * Vector3.Distance(m_Ball.transform.position, m_PointOfGravity1.position), ForceMode.Force);
            }
        }
    }
}
