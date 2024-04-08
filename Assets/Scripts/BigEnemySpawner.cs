using UnityEngine;

public class BigEnemySpawner : MonoBehaviour
{
    public GameObject tankPrefab;
    public float spawnInterval = 30f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTank();
            timer = 0f;
        }
    }

    void SpawnTank()
    {
        Instantiate(tankPrefab, transform.position, Quaternion.identity);
    }
}