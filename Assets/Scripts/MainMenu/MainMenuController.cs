using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private List<GameObject> m_Lighs = new List<GameObject>();
    public Transform m_LightsParent;
    public int m_FirstLight;
    public int m_LastLight;
    public float m_TimeToChangeLights;
    private float m_RemainingTimeToChangeLights;

    private void Awake()
    {
        for (int i = 0; i < m_LightsParent.childCount; i++)
        {
            GameObject go = m_LightsParent.GetChild(i).gameObject;
            if (i < m_LastLight)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
            m_Lighs.Add(go);
        }
        m_RemainingTimeToChangeLights = m_TimeToChangeLights;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if( m_RemainingTimeToChangeLights <= 0)
        {
            ChangeLights();
            m_RemainingTimeToChangeLights = m_TimeToChangeLights;
        }
        else
        {
            m_RemainingTimeToChangeLights -= dt;
        }
    }

    private void ChangeLights()
    {
        for (int i = 0; i < m_Lighs.Count; i++)
        {
            if(CheckActivationOfLight(i))
            {
                m_Lighs[i].gameObject.SetActive(true);
            }
            else
            {
                m_Lighs[i].gameObject.SetActive(false);
            }
        }
        m_FirstLight++;
        if(m_FirstLight >= m_Lighs.Count)
        {
            m_FirstLight = 0;
        }
        m_LastLight++;
        if (m_LastLight >= m_Lighs.Count)
        {
            m_LastLight = 0;
        }
    }

    private bool CheckActivationOfLight(int index)
    {
        bool res = false;
        if(index >= m_FirstLight && index <= m_LastLight)
        {
            res = true;
        }
        else if(m_FirstLight > m_LastLight && (index >= m_FirstLight || index <= m_LastLight))
        {
            res = true;
        }
        return res;
    }
}
