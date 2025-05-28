using UnityEditor;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    public float m_spawnInterval = 2f;
    public int m_MaxSpawns = 20;
    public float m_Timer;
    public int m_Counter = 0;
    public Transform m_SpawnPosition;

    private Vector3 m_LastPosition;

    private PowerUpsPoolManager m_PowerUpsPoolManager;

    private void Awake()
    {
        m_PowerUpsPoolManager = FindFirstObjectByType<PowerUpsPoolManager>();
    }

    private void Start()
    {
        m_LastPosition = Vector3.zero;
    }

    void Update()
    {
        if (m_Counter >= m_MaxSpawns) { return; }
        Debug.Log("Test");
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_spawnInterval)
        {
            m_Timer = 0f;
            if (m_SpawnPosition == null)
            {
                return;
            }
            Vector3 newPos = Vector3.zero;
            do
            {
                newPos = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
            }
            while (CheckNewPosition(newPos));
            m_LastPosition = newPos;
            Vector3 positionRandom = m_SpawnPosition.position + newPos;
            GameObject pu = m_PowerUpsPoolManager.Take();
            pu.transform.position = positionRandom;
            pu.SetActive(true);
            m_Counter++;
        }
    }

    private bool CheckNewPosition(Vector3 newPos)
    {
        return (m_LastPosition.x - newPos.x < 1f && m_LastPosition.z - newPos.z < 1f);
    }
}

