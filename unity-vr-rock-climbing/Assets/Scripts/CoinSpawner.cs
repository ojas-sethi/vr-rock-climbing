using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;

    public Transform[] climbingWalls;

    public int maxCoinsPerWall = 10; // Can change this max coins per wall

    public float baseSpawnInterval = 5f; // Can change this interval

    public Vector2 spawnIntervalRange = new Vector2(2f, 5f);

    private int numSpawnAreas;

    private void Start()
    {
        numSpawnAreas = climbingWalls.Length;
        for (int i = 0; i < numSpawnAreas; i++)
        {
            StartCoroutine(SpawnCoinsRoutine(i));
        }
    }

    private IEnumerator SpawnCoinsRoutine(int wallIndex)
    {
        while (true)
        {
            // Randomly select spawn interval within the specified range
            float spawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);

            for (int i = 0; i < maxCoinsPerWall; i++)
            {
                SpawnCoin(wallIndex);
                yield return new WaitForSeconds(spawnInterval);
            }
            // Wait for a random interval before resuming spawning
            yield return new WaitForSeconds(Random.Range(spawnInterval * 2, spawnInterval * 3));
        }
    }

    // Spawn a single coin at a random position within the spawn area
    private void SpawnCoin(int wallIndex)
    {
        // Calculate a random position within the spawn area of the specified wall
        Vector3 randomPosition = new Vector3(
            Random.Range(-climbingWalls[wallIndex].localScale.x / 2, climbingWalls[wallIndex].localScale.x / 2),
            Random.Range(-climbingWalls[wallIndex].localScale.y / 2, climbingWalls[wallIndex].localScale.y / 2),
            Random.Range(-climbingWalls[wallIndex].localScale.z / 2, climbingWalls[wallIndex].localScale.z / 2)
        ) + climbingWalls[wallIndex].position;

        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, randomPosition, Quaternion.identity, climbingWalls[wallIndex]);
        }
        else
        {
            Debug.LogError("Coin prefab is not assigned.");
        }
    }
}