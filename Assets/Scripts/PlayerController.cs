using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //makes gameover to false
    public bool gameOver = false;
    //checks if the player is on the road and makes it true
    public bool isOnRoad = true;

    //positions on the road where the player can move too
    private Vector3 middlePosition = new Vector3(1, 0, 0);
    private Vector3 leftPosition = new Vector3(-11.5f, 0, 0);
    private Vector3 rightPosition = new Vector3(13.5f, 0, 0);

    private Animator playerAnim;
    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier;
    private int positionState = 1;

    private Vector3 targetPosition; 
    public float sideMoveSpeed = 5f;

    void Start()
    {
        // makes sure that gameOver is false at the start
        gameOver = false;
        //starts in the middle
        transform.position = middlePosition;
        targetPosition = middlePosition;
        //gets the players rb
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        //get player animator component
        playerAnim = GetComponent<Animator>();

        //freezes players roattion so it avoids player
        playerRb.freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnRoad && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnRoad = false;
            playerAnim.SetTrigger("Jump_trig");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (positionState == 1)
            {
                targetPosition = leftPosition;
                positionState = 0;
            }
            else if (positionState == 2)
            {
                targetPosition = middlePosition;
                positionState = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (positionState == 1)
            {
                targetPosition = rightPosition;
                positionState = 2;
            }
            else if (positionState == 0)
            {
                targetPosition = middlePosition;
                positionState = 1;
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * sideMoveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            isOnRoad = true;
        }
        else if (collision.gameObject.CompareTag("Enemybarrier") 
                 || collision.gameObject.CompareTag("Enemyarmorcar") 
                 || collision.gameObject.CompareTag("Enemybus") 
                 || collision.gameObject.CompareTag("EnemyCar"))
        {
            gameOver = true;
            playerAnim.SetTrigger("GameOver"); 
            Debug.Log("Game Over!");

            playerRb.useGravity = true;
            playerRb.velocity = Vector3.zero;

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }
}
