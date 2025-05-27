using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private List<GameObject> m_Lighs = new List<GameObject>();

    public Transform m_LightsParent;

    private void Awake()
    {
        for (int i = 0; i < m_LightsParent.childCount; i++)
        {
            m_Lighs.Add(m_LightsParent.GetChild(i).gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
