using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private BoxCollider m_BoxCollider;
    public Transform m_RespawnPoint;
    public float m_ForceAmount = 10f;


    void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BALL")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.Sleep();// Para la velocidad y rotacion 
            other.transform.position = m_RespawnPoint.position;

            Vector3 direction = (transform.position - m_RespawnPoint.position).normalized;
            rb.AddForce(direction * m_ForceAmount, ForceMode.Impulse);

        }
    }
}
