using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject selectedCell;
    public static GameObject targetCell;

    public float speed = 20;
    private static int m_playerMovementpoint = 3;
    public static int playerMovementPoint
    {
        get { return m_playerMovementpoint; }
        set { m_playerMovementpoint = value;}
    }

    public static Vector3 playerPosition;
    private static bool playerIsMoving;
    private int xOffset;
    private int zOffset;

    void Start()
    {
        playerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //When Mouse Button is pressed the last cell will be targeted as the next position
        if (Input.GetMouseButtonDown(0) && targetCell != null && !playerIsMoving && TurnSystem.isPlayerTurn && playerMovementPoint > 0)
        {
            selectedCell = targetCell;
            Debug.Log(selectedCell.transform.position);
            playerIsMoving = true;
            StartCoroutine(MovePlayerBasedOnPath(PathFindingPlayer(selectedCell.transform.position)));
        }
        
    }

    public static bool IsThereAnObstacleOnPath()
    {
        RaycastHit hit;
        Debug.DrawRay(playerPosition, (targetCell.transform.position - playerPosition) * 15, Color.blue);
        if (Physics.Raycast(playerPosition, targetCell.transform.position - playerPosition, out hit, 15))
        {
            if(hit.collider.CompareTag("Obstacle") || hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("There is an obstacle on the path");
                return true;
            }
        }
        return false;
    }

    IEnumerator MovePlayerBasedOnPath(List<Vector3> path)
    {
        
        Debug.Log("Moving Player on the path...");
        foreach (Vector3 cell in path)
        {
            for (float ft = 0f; ft <= 1; ft += Time.fixedDeltaTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, cell, ft);
                yield return new WaitForFixedUpdate();
            }

        }
        playerIsMoving = false;
        playerPosition = transform.position;
        Debug.Log("Player isn't moving anymore");
    }

    List<Vector3> PathFindingPlayer(Vector3 selectedCell)
    {
        Debug.Log("Checking for Player best path");
        List<Vector3> xCellPaths = new List<Vector3>();
        //Creating a path going x Axis & a path going on Z axis
        for (int i = 0; i < playerMovementPoint; i++)
        {
            if (i == 0)
            {
                xCellPaths.Add(cellPath(transform.position.x, transform.position.z));
            }
            else
            {
                xCellPaths.Add(cellPath(xCellPaths[xCellPaths.Count - 1].
                x, xCellPaths[xCellPaths.Count - 1].z));
            }

        }
        Debug.Log("xPath :" + xCellPaths.Count);
        List<Vector3> uniqueXCellPath = xCellPaths.Distinct().ToList<Vector3>();
        playerMovementPoint -= uniqueXCellPath.Count;
        return uniqueXCellPath;

        //this nested Function are checking if the cell can be used by the enemy
        Vector3 cellPath(float xPosition, float zPosition)
        {
            int xOffsetWithSelectedCell = Convert.ToInt16(selectedCell.x - xPosition);
            int zOffsetWithSelectedCell = Convert.ToInt16(selectedCell.z - zPosition);

            bool xIsPositive = xOffsetWithSelectedCell > 0;
            bool zIsPositive = zOffsetWithSelectedCell > 0;


            RaycastHit hitForward;
            RaycastHit hitRight;
            Vector3 originPosition = new Vector3(xPosition, 2, zPosition);

            if (xPosition != selectedCell.x)
            {
                if (Physics.Raycast(originPosition, xIsPositive ? Vector3.right : Vector3.left, out hitRight, 5))
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
            }
            else if (zPosition != selectedCell.z)
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
            }
            else
            {
                return originPosition;
            }

        }
    }





}
