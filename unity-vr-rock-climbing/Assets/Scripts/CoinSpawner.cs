using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Prefab for the coin object
    public GameObject coinPrefab; // Assign in inspector

    // Parent GameObjects of the climbing walls
    public Transform[] climbingWalls; // Assign in inspector

    // Maximum number of coins that can be spawned per wall
    public int maxCoinsPerWall = 10;

    // Base spawn interval for coins
    public float baseSpawnInterval = 5f;

    // Randomized range for fluctuating spawn intervals
    public Vector2 spawnIntervalRange = new Vector2(2f, 5f);

    // Number of spawn areas (climbing walls)
    private int numSpawnAreas;

    // Start is called before the first frame update
    private void Start()
    {
        numSpawnAreas = climbingWalls.Length;
        // Start spawning coins for each spawn area
        for (int i = 0; i < numSpawnAreas; i++)
        {
            StartCoroutine(SpawnCoinsRoutine(i));
        }
    }

    // Coroutine for spawning coins at regular intervals for a specific spawn area
    private IEnumerator SpawnCoinsRoutine(int wallIndex)
    {
        while (true)
        {
            // Randomly select spawn interval within the specified range
            float spawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);

            // Spawn coins at regular intervals
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

        // Instantiate a coin at the calculated position, as a child of the specified climbing wall
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, randomPosition, Quaternion.identity, climbingWalls[wallIndex]);
        }
        else
        {
            // Log an error if the coinPrefab is not assigned
            Debug.LogError("Coin prefab is not assigned.");
        }
    }
}