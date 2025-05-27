using UnityEditor;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    public float m_spawnInterval = 2f;
    public int maxSpawns = 20;
    public float timer;
    public int counter = 0;
    public Transform SpawnPosition;

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
        if (counter >= maxSpawns) { return; }
        Debug.Log("Test");
        timer += Time.deltaTime;
        if (timer >= m_spawnInterval)
        {
            timer = 0f;
            if (SpawnPosition == null)
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
            Vector3 positionRandom = SpawnPosition.position + newPos;
            GameObject pu = m_PowerUpsPoolManager.Take();
            pu.transform.position = positionRandom;
            pu.SetActive(true);
            counter++;
        }
    }

    private bool CheckNewPosition(Vector3 newPos)
    {
        return (m_LastPosition.x - newPos.x < 1f && m_LastPosition.z - newPos.z < 1f);
    }
}

