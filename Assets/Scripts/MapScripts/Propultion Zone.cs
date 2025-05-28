using UnityEngine;

public class PropultionZone : MonoBehaviour
{
    public float m_ForceAmount = 10f;

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
            rb.AddForce(transform.right * m_ForceAmount, ForceMode.Force);
        }
    }
}
