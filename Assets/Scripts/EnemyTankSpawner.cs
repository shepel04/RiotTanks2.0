using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class EnemyTankSpawner : MonoBehaviour
{
    public GameObject EnemyTankPrefab; 
    public Transform[] EnemySpawnPoints;
    public float SpawnInterval = 1f; 
    public int MaxEnemies = 2;

    private int _currentEnemies = 0; 
    private bool _spawning;

    void Start()
    {
        StartSpawning();
    }

    void StartSpawning()
    {
        if (_currentEnemies < MaxEnemies)
        {
            Invoke("SpawnEnemy", SpawnInterval);
            _spawning = true;
        }
    }

    void SpawnEnemy()
    {
        List<Transform> availableSpawnPoints = new List<Transform>();
        foreach (Transform spawnPoint in EnemySpawnPoints)
        {
            if (spawnPoint.childCount == 0) 
            {
                availableSpawnPoints.Add(spawnPoint);
            }
        }

        if (availableSpawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            GameObject enemyTank = Instantiate(EnemyTankPrefab, availableSpawnPoints[randomIndex].position, Quaternion.identity);
            enemyTank.transform.parent = availableSpawnPoints[randomIndex]; 
            _currentEnemies++;
        }

        if (_currentEnemies < MaxEnemies)
        {
            Invoke("SpawnEnemy", SpawnInterval);
        }
        else
        {
            _spawning = false;
        }
    }

    public void EnemyDestroyed()
    {
        _currentEnemies--;
        if (!_spawning && _currentEnemies < MaxEnemies)
        {
            StartSpawning();
        }
    }
}