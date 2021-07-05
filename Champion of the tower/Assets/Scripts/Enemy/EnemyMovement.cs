using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 20;

    private int xOffset;
    private int zOffset;
    private bool directionShouldBeRight;

    private bool enemyIsMoving;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnSystem.isEnemyTurn && !enemyIsMoving)
        {
            
            Debug.DrawLine(transform.position, PlayerMovement.playerPosition, Color.cyan);
            xOffset = Convert.ToInt16(PlayerMovement.playerPosition.x - transform.position.x);
            zOffset = Convert.ToInt16(PlayerMovement.playerPosition.z - transform.position.z);

            RaycastHit hitForward;
            RaycastHit hitRight;
            if (Physics.Raycast(transform.position, zOffset > 0 ? Vector3.forward : Vector3.back, out hitForward, 10))
            {
                if (hitForward.collider.CompareTag("Obstacle"))
                {
                    Debug.Log("Obstacle on the forward way !");
                    directionShouldBeRight = true;
                }
            }
            if (Physics.Raycast(transform.position, xOffset > 0 ? Vector3.right : Vector3.left, out hitRight, 10))
            {
                if (hitRight.collider.CompareTag("Obstacle"))
                {
                    Debug.Log("Obstacle on The Right way");
                    directionShouldBeRight = false;
                }
            }
            enemyIsMoving = true;
        }

        if (enemyIsMoving)
        {
            MovePlayerToSelectedCell(xOffset, zOffset);
        }
    }

    private void MovePlayerToSelectedCell(int xOffset, int zOffset)
    {



        if (directionShouldBeRight)
        {
            if (xOffset > 0 ? transform.position.x < PlayerMovement.playerPosition.x : transform.position.x > PlayerMovement.playerPosition.x)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed * (xOffset > 0 ? 1 : -1));
            }
            else if (zOffset > 0 ? transform.position.z < PlayerMovement.playerPosition.z : transform.position.z > PlayerMovement.playerPosition.z)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed * (zOffset > 0 ? 1 : -1));
            }
            else
            {
                // Center the Player position in the Cell and Stopping the movement.
                enemyIsMoving = false;
            }
        }
        else
        {
            if (zOffset > 0 ? transform.position.z < PlayerMovement.playerPosition.z : transform.position.z > PlayerMovement.playerPosition.z)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed * (zOffset > 0 ? 1 : -1));
            }
            else if (xOffset > 0 ? transform.position.x < PlayerMovement.playerPosition.x : transform.position.x > PlayerMovement.playerPosition.x)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed * (xOffset > 0 ? 1 : -1));
            }
            else
            {
                // Center the Player position in the Cell and Stopping the movement.
                enemyIsMoving = false;
            }
        }


    }


}
