using UnityEngine;

public class PropultionZone : MonoBehaviour
{
    public float forceAmount = 10f; // Fuerza en la dirección X

    public AudioManager m_AudioManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BALL")
        {
            m_AudioManager.PlaySFX(m_AudioManager.m_SpeedZoneSound);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null && other.tag == "BALL")
        {
            // Aplica una fuerza constante solo en la dirección X
            rb.AddForce(transform.right * forceAmount, ForceMode.Force);
        }
    }
}
