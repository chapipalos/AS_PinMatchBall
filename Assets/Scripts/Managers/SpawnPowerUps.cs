using UnityEditor;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    public GameObject freezePowerUpPrefab;
    public GameObject wheelPowerUpPrefab;
    public GameObject accelerationPowerUpPrefab;
    public GameObject spikeBallPowerUpPrefab;
    public GameObject ghostPowerUpPrefab;
    public GameObject AntigoalPowerUpPrefab;
    public GameObject MagnetPowerUpPrefab;

    public float m_spawnInterval = 2f;
    public int maxSpawns = 20;
    private float timer;
    private int counter = 0;
    public Transform SpawnPosition;



    void Update()
    {
        if (counter >= maxSpawns) { return; }
        timer += Time.deltaTime;
        if (timer >= m_spawnInterval)
        {
            timer = 0f;
            if (SpawnPosition == null)
            {
                return;
            }
            Vector3 positionRandom = SpawnPosition.position + new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
            GameObject PrefaboUp = GetRandomPoUP();
            if (PrefaboUp != null)
            {
                Instantiate(PrefaboUp, positionRandom, Quaternion.identity);
                counter++;
            }
        }
    }

    private GameObject GetRandomPoUP()
    {
        GameObject[] prefabs = new GameObject[]
        {
            freezePowerUpPrefab,
            wheelPowerUpPrefab,
            accelerationPowerUpPrefab,
            spikeBallPowerUpPrefab,
            ghostPowerUpPrefab,
            AntigoalPowerUpPrefab,
            MagnetPowerUpPrefab
        };

        //If is empty, don't spawn, only valid prefabs
        var validPrefabs = System.Array.FindAll(prefabs, p => p != null);

        if (validPrefabs.Length == 0) return null;

        int randomIndex = Random.Range(0, validPrefabs.Length);
        return validPrefabs[randomIndex];
    }
}

