using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 20;
    public int enemyMovementPoint;
    public bool isEnemyTurn = false;
    private bool enemyIsMoving;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (isEnemyTurn && !enemyIsMoving && enemyMovementPoint > 0)
        {
            enemyIsMoving = true;
            StartCoroutine(MoveEnemyBasedOnPath(PathFindingEnemy()));
        }

    }


    List<Vector3> PathFindingEnemy()
    {
        Debug.Log("Checking for Enemy best path");
        List<Vector3> rightCellPaths = new List<Vector3>();
        List<Vector3> forwardCellPaths = new List<Vector3>();
        //Creating a path going x Axis & a path going on Z axis
        for (int i = 0; i < enemyMovementPoint; i++)
        {
            if(i == 0)
            {
                rightCellPaths.Add(cellPath(transform.position.x, transform.position.z, "Right"));
                forwardCellPaths.Add(cellPath(transform.position.x, transform.position.z, "Forward"));
            } else
            {
                rightCellPaths.Add(cellPath(rightCellPaths[rightCellPaths.Count  -1].
                x, rightCellPaths[rightCellPaths.Count - 1].z, "Right"));
                forwardCellPaths.Add(cellPath(forwardCellPaths[forwardCellPaths.Count - 1].x, forwardCellPaths[forwardCellPaths.Count - 1].z, "Forward"));
            }
            
        }
        // Removing dupplicate value cerated on the loop to have the smallest path
        List<Vector3> uniqueXCellPath = rightCellPaths.Distinct().ToList<Vector3>();
        List<Vector3> uniqueZCellPath = forwardCellPaths.Distinct().ToList<Vector3>();
        Debug.Log("uniqueXPath :" + uniqueXCellPath.Count);
        Debug.Log("uniqueZPath :" + uniqueZCellPath.Count);
        // Returning the path based last cell of the path and his distance with playerPositon.
        bool forwardPathIsChoosen = Vector3.Distance(uniqueXCellPath[uniqueXCellPath.Count - 1], PlayerMovement.playerPosition) > Vector3.Distance(uniqueZCellPath[uniqueZCellPath.Count - 1], PlayerMovement.playerPosition);
        Debug.Log("Z Path is Choosen : " + forwardPathIsChoosen);
        return forwardPathIsChoosen ? uniqueZCellPath : uniqueXCellPath;


        //this nested Function are checking if the cell can be used by the enemy
        Vector3 cellPath(float xPosition, float zPosition, string axis)
        {
            int xOffsetWithPlayer = Convert.ToInt16(PlayerMovement.playerPosition.x - xPosition);
            int zOffsetWithPlayer = Convert.ToInt16(PlayerMovement.playerPosition.z - zPosition);

            bool xIsPositive = xOffsetWithPlayer > 0;
            bool zIsPositive = zOffsetWithPlayer > 0;


            RaycastHit hitForward;
            RaycastHit hitRight;

            // this next block is checking if the cell is currently where it should be (next to the player)
            Vector3 primaryTargetCell;
            Vector3 secondaryTargetCell;
            Vector3 originPosition = new Vector3(xPosition, 2, zPosition);
            if (xOffsetWithPlayer != zOffsetWithPlayer)
            {
                primaryTargetCell = PlayerMovement.playerPosition + new Vector3(xOffsetWithPlayer > zOffsetWithPlayer?(xIsPositive ? -5 : 5) : 0, 0,zOffsetWithPlayer > xOffsetWithPlayer ? (zIsPositive ? -5 : 5) : 0);
                secondaryTargetCell = PlayerMovement.playerPosition + new Vector3(zOffsetWithPlayer > xOffsetWithPlayer ? (xIsPositive ? -5 : 5) : 0, 0, xOffsetWithPlayer > zOffsetWithPlayer ? (zIsPositive ? -5 : 5) : 0);
            } else
            {
                primaryTargetCell = PlayerMovement.playerPosition + new Vector3(xIsPositive ? -5 : 5, 0, 0);
                secondaryTargetCell = PlayerMovement.playerPosition + new Vector3(0, 0, zIsPositive ? -5 : 5);
            }

            // if the cell is on the targetCell (primary or secondary, we will return his position)
            if (originPosition.x == primaryTargetCell.x && originPosition.z == primaryTargetCell.z)
            {
                return originPosition;
            }
            else if (originPosition.x == secondaryTargetCell.x && originPosition.z == secondaryTargetCell.z)
            {
                return originPosition;
            }



            if (axis == "Right")
            {
                if (Physics.Raycast(originPosition,xIsPositive ? Vector3.right : Vector3.left, out hitRight, 5))
                {
                    if (Physics.Raycast(originPosition, zIsPositive ? Vector3.forward : Vector3.back, out hitForward, 5))
                    {
                        return new Vector3(originPosition.x + 5 * (xIsPositive ? -1 : 1), originPosition.y, originPosition.z);
                    }
                    return new Vector3(originPosition.x, originPosition.y, originPosition.z + 5 * (zIsPositive ? 1 : -1));
                }
                else
                {
                    return new Vector3(originPosition.x + 5 * (xIsPositive ? 1 : -1), originPosition.y, originPosition.z);
                }
            } else if (axis == "Forward")
            {
                if (Physics.Raycast(originPosition, zIsPositive ? Vector3.forward : Vector3.back, out hitForward, 5))
                {
                    if (Physics.Raycast(originPosition, xIsPositive ? Vector3.right : Vector3.left, out hitRight, 5))
                    {
                        return new Vector3(originPosition.x + 5 * (xIsPositive ? -1 : 1), originPosition.y, originPosition.z);
                    }
                    return new Vector3(originPosition.x + 5 * (xIsPositive ? 1 : -1), originPosition.y, originPosition.z);
                }
                else
                {
                    return new Vector3(originPosition.x, originPosition.y, originPosition.z + 5 * (zIsPositive ? 1 : -1));
                }
            } else 
            {
                return originPosition;
            }

        }
    }


    IEnumerator MoveEnemyBasedOnPath(List<Vector3> path)
    {
        Debug.Log("Moving enemies on the path...");
        foreach (Vector3 cell in path)
        {
            for(float ft = 0f; ft <= 1; ft += Time.fixedDeltaTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, cell, ft);
                yield return new WaitForFixedUpdate();
            }
            
        }
        enemyMovementPoint = 0;
        enemyIsMoving = false;
        Debug.Log("Enemy isn't moving anymore");
    }


    






}
