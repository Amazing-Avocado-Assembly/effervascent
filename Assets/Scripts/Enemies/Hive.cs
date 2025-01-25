using System.Collections;
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Projectile")) && Time.time - lastSpawnTime > coolDown)
        {
            // Spawn enemies
            int spawnCount = Random.Range(enemySpawnCount.x, enemySpawnCount.y + 1);
            StartCoroutine(DelayedSpawn(spawnCount));
            lastSpawnTime = Time.time;
        }
    }

    IEnumerator DelayedSpawn(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
