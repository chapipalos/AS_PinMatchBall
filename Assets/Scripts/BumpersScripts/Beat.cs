using UnityEngine;
using UnityEngine.Rendering;

public class Beat : MonoBehaviour
{

    public float m_ExpansionTime = 2f;
    public float m_ContractionTime = 1f;
    public Vector3 m_InitialScale;
    public Vector3 m_FinalScale;
    public bool m_ActivateExpansion = false;

    private float m_InitialTime;

    void Update()
    {
        if (m_ActivateExpansion)
        {
            float cicloTotal = m_ExpansionTime + m_ContractionTime;
            float tiempoCiclo = (Time.time - m_InitialTime) % cicloTotal;

            float factor;

            if (tiempoCiclo < m_ExpansionTime)
            {
                factor = tiempoCiclo / m_ExpansionTime;
            }
            else
            {
                factor = 1 - ((tiempoCiclo - m_ExpansionTime) / m_ContractionTime);
            }

            transform.localScale = Vector3.Lerp(m_InitialScale, m_FinalScale, factor);

            if (tiempoCiclo >= cicloTotal - Time.deltaTime)
            {
                m_ActivateExpansion = false;
            }
        }
    }

    public void InitExpansion()
    {
        if (!m_ActivateExpansion)
        {
            m_ActivateExpansion = true;
            m_InitialTime = Time.time;
        }
    }
}
