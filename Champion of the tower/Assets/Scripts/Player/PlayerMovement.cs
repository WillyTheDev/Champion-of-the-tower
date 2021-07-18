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

    public static Vector3 playerPosition;
    public static bool playerIsMoving;

    void Start()
    {
        playerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        //When Mouse Button is pressed the last cell will be targeted as the next position
        if (Input.GetMouseButtonDown(0) && targetCell != null && !playerIsMoving && TurnSystem.isPlayerTurn && PlayerData.playerMovementPoint > 0)
        {
            selectedCell = targetCell;
            Debug.Log(selectedCell.transform.position);
            playerIsMoving = true;
            StartCoroutine(MovePlayerBasedOnPath(PathFindingPlayer(selectedCell.transform.position)));
        }
        
    }

    

    IEnumerator MovePlayerBasedOnPath(List<Vector3> path)
    {
        CellData.cellPath = new List<Vector3>();
        playerPosition = path[path.Count - 1];
        Debug.Log("Moving Player on the path...");
        foreach (Vector3 cell in path)
        {
            for (float ft = 0f; ft <= 1; ft += Time.fixedDeltaTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, cell, ft);
                yield return new WaitForFixedUpdate();
            }
            PlayerData.playerMovementPoint -= 1;
        }
        playerPosition = transform.position;
        playerIsMoving = false;
        Debug.Log("Player isn't moving anymore");
    }

    public static List<Vector3> PathFindingPlayer(Vector3 selectedCell)
    {
        Debug.Log("Checking for Player best path");
        List<Vector3> xCellPaths = new List<Vector3>();
        //Creating a path going x Axis & a path going on Z axis
        for (int i = 0; i < PlayerData.playerMovementPoint; i++)
        {
            if (i == 0)
            {
                xCellPaths.Add(cellPath(playerPosition.x, playerPosition.z));
            }
            else
            {
                xCellPaths.Add(cellPath(xCellPaths[xCellPaths.Count - 1].
                x, xCellPaths[xCellPaths.Count - 1].z));
            }

        }
        Debug.Log("xPath :" + xCellPaths.Count);
        List<Vector3> uniqueXCellPath = xCellPaths.Distinct().ToList<Vector3>();
        
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
