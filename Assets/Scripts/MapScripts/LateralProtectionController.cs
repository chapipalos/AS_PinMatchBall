using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LateralProtectionController : MonoBehaviour
{
    public bool m_IsActivated;

    private Vector3 m_FinalPosition;

    public BarrierController m_Barrier;

    private void Start()
    {
        m_IsActivated = false;
        m_FinalPosition = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    private void Update()
    {
        if (m_IsActivated)
        {
            if (Vector3.Distance(transform.position, m_FinalPosition) > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, m_FinalPosition, Time.deltaTime * 2);
            }
            else
            {
                transform.position = m_FinalPosition;
                m_Barrier.m_Activate = true;
            }

        }
    }
}
