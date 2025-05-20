using UnityEngine;

public class SplashController : MonoBehaviour
{
    private float m_SplashCounter = 5f;

    public GameObject[] m_SplashList;
    private float m_Gamma;

    public GameObject m_Parent;

    // Update is called once per frame
    void Update()
    {
        if (m_Parent.tag == "Player1" && GameManager.m_RedSplash)
        {
            m_Gamma = 1f;
            foreach (GameObject splash in m_SplashList)
            {
                m_Gamma = (float)(m_SplashCounter / 5f);
                m_Gamma = Mathf.Clamp(m_Gamma, 0f, 1f);
                splash.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, m_Gamma);
                if (m_Gamma == 0)
                {
                    GameManager.m_RedSplash = false;
                }
            }
        }
        else if (m_Parent.tag == "Player2" && GameManager.m_BlueSplash)
        {
            m_Gamma = 1f;
            foreach (GameObject splash in m_SplashList)
            {
                m_Gamma = (float)(m_SplashCounter / 5f);
                m_Gamma = Mathf.Clamp(m_Gamma, 0f, 1f);
                splash.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, m_Gamma);
                if (m_Gamma == 0)
                {
                    GameManager.m_BlueSplash = false;
                }
            }
        }
        else
        {
            m_Gamma = 0f;
            foreach (GameObject splash in m_SplashList)
            {
                splash.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, m_Gamma);
            }
        }
    }

    public void SplashAction()
    {
        if (m_Parent.tag == "Player2" && GameManager.m_BlueSplash)
        {
            m_SplashCounter--;
        }
        else if (m_Parent.tag == "Player1" && GameManager.m_RedSplash)
        {
            m_SplashCounter--;
        }
    }
}
