using UnityEngine;

public class SpawnEnemiesManager : MonoBehaviour
{
    public GameObject[] enemigosPrefabs;
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnEnemigos();
    }

    private void SpawnEnemigos()
    {
        int cantidad = Random.Range(1, spawnPoints.Length + 1);
    
        for (int i = 0; i < cantidad; i++)
        {
            GameObject prefabAleatorio = enemigosPrefabs[Random.Range(0, enemigosPrefabs.Length)];
            Instantiate(prefabAleatorio, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}
