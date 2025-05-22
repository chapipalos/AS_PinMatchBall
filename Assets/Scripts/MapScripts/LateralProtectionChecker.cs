using UnityEngine;

public class LateralProtectionChecker : MonoBehaviour
{
    public GameObject m_Protection;
    public GameObject m_Stick;
    public bool m_ActiveSticks=false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BALL"))
        {
            m_Protection.GetComponent<LateralProtectionController>().m_IsActivated = true;
           m_ActiveSticks = true;
           
        }
    }
    private void Update()
    {
        if (m_ActiveSticks == true)
        {
            m_Stick.GetComponent<MoveSticksManager>().MoveIf = true;
        }
    }
}
