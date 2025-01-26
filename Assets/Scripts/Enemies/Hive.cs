using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Hive : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField, MinMaxSlider(1, 10)] private Vector2Int enemySpawnCount = new Vector2Int(1, 5);
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float coolDown = 5.0f;
    [SerializeField] private float spawnDelay = 0.01f;

    float lastSpawnTime = 0;

    List<GameObject> spawnedEnemies = new List<GameObject>();

    void OnCollisionEnter2D(Collision2D other)
    {
        if (Time.time - lastSpawnTime > coolDown)
        {
            // Spawn enemies
            int spawnCount = Random.Range(enemySpawnCount.x, enemySpawnCount.y + 1);
            StartCoroutine(DelayedSpawn(spawnCount));
            lastSpawnTime = Time.time;
        }
    }

    IEnumerator DelayedSpawn(int spawnCount)
    {
        // Delete dead enemies 
        spawnedEnemies.RemoveAll(enemy => enemy == null);
        spawnCount = Mathf.Min(spawnCount, enemySpawnCount.y - spawnedEnemies.Count);
        for (int i = 0; i < spawnCount; i++)
        {
            var enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
