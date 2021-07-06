using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 20;
    public int enemyMovementPoint;
    private int xOffset;
    private int zOffset;
    private bool xPriority;
    private bool directionShouldBeRight;
    private Vector3 targetPosition;
    private Vector3 enemyInitialPosition;
    private bool enemyIsMoving;


    void Start()
    {
        enemyInitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (TurnSystem.isEnemyTurn && !enemyIsMoving && enemyMovementPoint > 0)
        {
            DefineTargetCellForEnemyMovement();
        }

        if (enemyIsMoving)
        {
            MovePlayerToSelectedCell(xOffset, zOffset);
        }
    }






    private void DefineTargetCellForEnemyMovement()
    {
        RaycastHit hitForward;
        RaycastHit hitRight;
        double movementPointDivided = enemyMovementPoint / 2;

        Debug.DrawLine(transform.position, PlayerMovement.playerPosition, Color.cyan);
        // we have for X and Z the distance between enemy and Player.
        xOffset = Convert.ToInt16(PlayerMovement.playerPosition.x - transform.position.x);
        zOffset = Convert.ToInt16(PlayerMovement.playerPosition.z - transform.position.z);
        // We are defining wich is the best cell to target based on the offset
        // If we are align on the x axis, then we will go on a straight line
        if (xOffset == 0)
        {
            Debug.Log("Enemy is supposed to go forward");
            xPriority = false;
            Debug.DrawRay(transform.position, xOffset > 0 ? Vector3.right : Vector3.left * (5 * enemyMovementPoint), Color.red);
            
                if (Physics.Raycast(transform.position, zOffset > 0 ? Vector3.forward : Vector3.back, out hitForward, 5 * enemyMovementPoint))
                {
                
                    if (hitForward.collider.CompareTag("Obstacle"))
                    {
                        Debug.Log("Enemy met an obstacle ! on Z Axis");
                        calculatePositionWhenNotAlign();
                    }
                    else
                    {
                        Debug.Log("There is no obstacle");
                        targetPosition = enemyInitialPosition + new Vector3(0, 0, 5 * enemyMovementPoint * (zOffset > 0 ? 1 : -1));
                    }

                
            }
            else
            {
                Debug.Log("Weird");
            }
            
        }
        // If we are align on the z axis, then we will go on a straight line
        else if (zOffset == 0)
        {
            xPriority = true;
            Debug.DrawRay(transform.position, transform.forward * (zOffset > 0 ? 20 : -20), Color.green);
            if (Physics.Raycast(transform.position, xOffset > 0 ? Vector3.right : Vector3.left, out hitRight, 5 * enemyMovementPoint))
                
            {
                if (hitRight.collider.CompareTag("Obstacle"))
                {
                    Debug.Log("Enemy met an obstacle ! on X Axis");
                    calculatePositionWhenNotAlign();
                } else
                {
                    targetPosition = enemyInitialPosition + new Vector3(5 * enemyMovementPoint * (xOffset > 0 ? 1 : -1), 0, 0);
                }
            } 
            
        }
        // If the player is not align vertically or horizontally
        // We mush check for is movementPoint and attribute them base on distance on both axis.
        else
        {
            calculatePositionWhenNotAlign();
        }

        void calculatePositionWhenNotAlign()
        {
            if (enemyMovementPoint % 2 != 0)
            {
                // if the distance on x Axis is longer than the z, then the enemy will move more on the longer axis.
                xPriority = (xOffset * (xOffset > 0 ? 1 : -1)) > (zOffset * (zOffset > 0 ? 1 : -1));

                float xPosition = Convert.ToSingle(5 * (xPriority ? Math.Ceiling(movementPointDivided) : Math.Floor(movementPointDivided)) * (xOffset > 0 ? 1 : -1));
                float zPosition = Convert.ToSingle(5 * (xPriority ? Math.Floor(movementPointDivided) : Math.Ceiling(movementPointDivided)) * (zOffset > 0 ? 1 : -1));

                targetPosition = enemyInitialPosition + new Vector3(xPosition, 0, zPosition);
            }
            else
            {
                targetPosition = enemyInitialPosition + new Vector3(5 * (enemyMovementPoint / 2) * (xOffset > 0 ? 1 : -1), 0, 5 * (enemyMovementPoint / 2) * (zOffset > 0 ? 1 : -1));
            }
        }
        // we were checking if there were obstacle on the path we defined, and prioritize an axis for avoiding this obstacle.
        
        if (Physics.Raycast(transform.position, zOffset > 0 ? Vector3.forward : Vector3.back, out hitForward, 5 * enemyMovementPoint))
        {
            if (hitForward.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Obstacle on the forward way !");
                directionShouldBeRight = true;
            }
        }
        if (Physics.Raycast(transform.position, xOffset > 0 ? Vector3.right : Vector3.left, out hitRight, 5 * enemyMovementPoint))
        {
            if (hitRight.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Obstacle on The Right way");
                directionShouldBeRight = false;
            }
        }
        // Once everything is define we can move the enemy
        enemyIsMoving = true;
        enemyMovementPoint = 0;
    }



    private void MovePlayerToSelectedCell(int xOffset, int zOffset)
    {
        
        Debug.Log("targetPosition :" + targetPosition);
        Debug.Log("Enemy Initial Position" + enemyInitialPosition);

        if (directionShouldBeRight)
        {
            if (xOffset > 0 ? transform.position.x < targetPosition.x : transform.position.x > targetPosition.x)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed * (xOffset > 0 ? 1 : -1));
            }
            else if (zOffset > 0 ? transform.position.z < targetPosition.z : transform.position.z > targetPosition.z)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed * (zOffset > 0 ? 1 : -1));
            }
            else
            {
                // Center the Player position in the Cell and Stopping the movement.
                enemyMovementPoint = 0;
                transform.position = targetPosition;
                enemyInitialPosition = transform.position;
                enemyIsMoving = false;
            }
        }
        else
        {
            if (zOffset > 0 ? transform.position.z < targetPosition.z : transform.position.z > targetPosition.z)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed * (zOffset > 0 ? 1 : -1));
            }
            else if (xOffset > 0 ? transform.position.x < targetPosition.x : transform.position.x > targetPosition.x)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed * (xOffset > 0 ? 1 : -1));
            }
            else
            {
                // Center the Player position in the Cell and Stopping the movement.
                enemyMovementPoint = 0;
                transform.position = targetPosition;
                enemyInitialPosition = transform.position;
                enemyIsMoving = false;
            }
        }


    }


}
