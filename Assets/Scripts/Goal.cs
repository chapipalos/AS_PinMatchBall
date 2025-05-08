using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private BoxCollider m_BoxCollider;
    public Transform m_RespawnPoint;
    public float m_ForceAmount = 10f;
    public GameObject particles;
    public GameObject Goalparticles;
 

    void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BALL")
        {
            if (Goalparticles != null)
            {
                Instantiate(Goalparticles, other.transform.position, Quaternion.identity);
            }
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.Sleep();// Para la velocidad y rotacion 
            other.transform.position = m_RespawnPoint.position;

            if (particles != null)
            {
                Instantiate(particles,m_RespawnPoint.position, Quaternion.identity);
            }
            Vector3 direction = (transform.position - m_RespawnPoint.position).normalized;
            rb.AddForce(direction * m_ForceAmount, ForceMode.Impulse);

        }
    
    }
}
