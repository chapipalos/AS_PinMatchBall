using System.Collections.Generic;
using UnityEngine;

public class PowerUpsPoolManager : MonoBehaviour
{
    public List<GameObject> m_PowerUps = new List<GameObject>();
    public GameObject m_PowerUpPrefab;
    public int m_PoolSize = 10;

    private void Awake()
    {
        for (int i = 0; i < m_PoolSize; i++)
        {
            GameObject go = Instantiate(m_PowerUpPrefab);
            go.SetActive(false);
            m_PowerUps.Add(go);
        }
    }

    public GameObject Take()
    {
        foreach (GameObject go in m_PowerUps)
        {
            if (!go.activeSelf)
            {
                return go;
            }
        }

        GameObject newObject = Instantiate(m_PowerUpPrefab);
        m_PowerUps.Add(newObject);
        return newObject;
    }

    public void Return(GameObject go)
    {
        go.SetActive(false);
    }
}
