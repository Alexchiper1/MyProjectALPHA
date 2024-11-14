using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 30;
    private float lowerBound = -15f;

    private PlayerController playerControllerScript;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerControllerScript = player.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (transform.position.z < lowerBound)
        {
            Destroy(gameObject);
        }
    }
}
