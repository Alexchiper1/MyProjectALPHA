using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  
    private float spawnPosZ = 300f;
    private float[] spawnPositionsX = new float[] { 12f, 1.5f, -12f };

    private PlayerController playerControllerScript;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerControllerScript = player.GetComponent<PlayerController>();
        }
        InvokeRepeating("SpawnRandomObstacle", 1f, 1f);
    }

    void SpawnRandomObstacle()
    {
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            float randomX = spawnPositionsX[Random.Range(0, spawnPositionsX.Length)];
            Vector3 spawnPosition = new Vector3(randomX, 0, spawnPosZ);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, obstaclePrefabs[obstacleIndex].transform.rotation);
        }
    }
}
