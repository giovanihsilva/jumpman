using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("Spawn")]
    public List<GameObject> obstaclePrefab;
    public float starDelay = 2f;
    public float spawnInterval = 1.5f;
    public Transform spawnPont;

    [Header("Game Over")]
    public bool IsGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), starDelay, spawnInterval);
    }

    void SpawnObstacle()
    {
        if (!IsGameOver && obstaclePrefab.Count > 0)
        {
            // escolher um objstaculo aleatorio
            int index = Random.Range(0, obstaclePrefab.Count);
            GameObject obstacleescolhido = obstaclePrefab[index];

            //spawn 
            Instantiate(obstacleescolhido,
                spawnPont.position,
                spawnPont.rotation);
        }

    }
}