using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRoad : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    public float speed = 15f;

    private PlayerController playerControllerScript;

    void Start()
    {
        startPos = transform.position;
        repeatWidth = 120;

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
            transform.Translate(Vector3.back * speed * Time.deltaTime);

            if (transform.position.z < startPos.z - repeatWidth)
            {
                transform.position = startPos;
            }
        }
    }
}
