using UnityEngine;

public class LateralProtectionChecker : MonoBehaviour
{
    public GameObject m_Protection;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BALL"))
        {
            m_Protection.GetComponent<LateralProtectionController>().m_IsActivated = true;
        }
    }
}
