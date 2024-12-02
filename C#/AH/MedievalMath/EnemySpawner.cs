using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject easyEnemyPrefab;
    public GameObject mediumEnemyPrefab;
    public GameObject hardEnemyPrefab;

    public Transform enemySpawnPoint;  // Where enemies spawn
    public float spawnInterval = 5.0f;  // Time between spawns
    private float elapsedTime = 0f;    // Timer to track spawning

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= spawnInterval)
        {
            SpawnEnemy();
            elapsedTime = 0f;  // Reset the timer
        }
    }

    void SpawnEnemy()
    {
        // Randomly choose an enemy difficulty to spawn
        int randomChoice = Random.Range(0, 3);
        GameObject enemyToSpawn = null;

        switch (randomChoice)
        {
            case 0:
                enemyToSpawn = easyEnemyPrefab;
                break;
            case 1:
                enemyToSpawn = mediumEnemyPrefab;
                break;
            case 2:
                enemyToSpawn = hardEnemyPrefab;
                break;
        }

        if (enemyToSpawn != null)
        {
            Instantiate(enemyToSpawn, enemySpawnPoint.position, Quaternion.identity);
        }
    }
}