using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Prefab for the coin object
    public GameObject coinPrefab; // Assign in inspector

    // Parent GameObject of the climbing wall
    public Transform climbingWall; // Assign in inspector

    // Maximum number of coins that can be spawned
    public int maxCoins = 10;

    // Time between spawns
    public float baseSpawnInterval = 5f;

    // Size of the area where coins can spawn
    public Vector3 spawnAreaSize = new Vector3(5, 5, 5); // TODO: Adjust based on the size of the wall

    // Current number of spawned coins
    private int currentCoins = 0;

    // Start is called before the first frame update
    private void Start()
    {
        // Start spawning coins
        StartCoroutine(SpawnCoinsRoutine());
    }

    // Coroutine for spawning coins at regular intervals
    private IEnumerator SpawnCoinsRoutine()
    {
        while (currentCoins < maxCoins)
        {
            // Spawn a coin
            SpawnCoin();

            // Calculate next spawn interval based on current number of coins and base spawn interval
            float spawnInterval = baseSpawnInterval / (currentCoins + 1);

            // Wait for the specified interval before spawning the next coin
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Spawn a single coin at a random position within the spawn area
    private void SpawnCoin()
    {
        // If the maximum number of coins has been reached, stop spawning
        if (currentCoins >= maxCoins)
        {
            return;
        }

        // Calculate a random position within the spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        ) + climbingWall.position; // Adjust position based on the climbing wall's position

        // Instantiate a coin at the calculated position, as a child of the climbing wall
        if (coinPrefab != null && climbingWall != null)
        {
            Instantiate(coinPrefab, randomPosition, Quaternion.identity, climbingWall);
            currentCoins++; // Increment the count of spawned coins
        }
        else
        {
            // Log an error if either the coinPrefab or climbingWall is not assigned
            Debug.LogError("Coin prefab or climbing wall is not assigned.");
        }
    }
}