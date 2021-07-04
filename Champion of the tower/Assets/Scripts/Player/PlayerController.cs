using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject selectedCell;
    public static GameObject targetCell;

    public float speed = 20;
    private static int m_playerMovementDistance = 15;
    public static int playerMovementDistance {
        get { return m_playerMovementDistance; }
        set {
            if (value % 5 == 0)
            {
                m_playerMovementDistance = value;
            }
            else
            {
                m_playerMovementDistance = 15;
                Debug.Log("Value can't be divised by 5!");
            }
        }
    }

    public static Vector3 playerPosition;
    private static bool playerIsMoving;

    private List<Vector3> path;
    private int xOffset;
    private int zOffset;
    public int xSteps;
    public int zSteps;

    void Start()
    {
        print("Sart Method is Launched");
        playerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //When Mouse Button is pressed the last cell will be targeted as the next position
        if (Input.GetMouseButtonDown(0) && targetCell != null && !playerIsMoving)
        {
            selectedCell = targetCell;
            Debug.Log(selectedCell.transform.position);
            // Offsets are used to calculate how many cells there is between player position and selectedCells
            // Offsets are used to determine if the movement will be negative or positive.
            xOffset = Convert.ToInt16(selectedCell.transform.position.x - transform.position.x);
            zOffset = Convert.ToInt16(selectedCell.transform.position.z - transform.position.z);
            print("xOffset: " + xOffset + " zOffset :" + zOffset);
            zSteps = Math.Abs(zOffset / 5);
            xSteps = Math.Abs(xOffset / 5);
            playerIsMoving = true;
        }
        if (playerIsMoving) {
            MovePlayerToSelectedCell(xOffset, zOffset);
        }
            
        
    }

    public static bool IsThereAnObstacleOnPath()
    {
        RaycastHit hit;
        Debug.DrawRay(playerPosition, (targetCell.transform.position - playerPosition) * 15, Color.blue);
        if (Physics.Raycast(playerPosition, targetCell.transform.position - playerPosition, out hit, 15))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("There is an obstacle on the path");
                return true;
            }
        }
        return false;
    }

    

    private void MovePlayerToSelectedCell(int xOffset, int zOffset)
    {
        //Steps will be use in further development
        

        bool isGoingUp = zOffset > 0;
        bool isGoingRight = xOffset > 0;
        Debug.Log("Player is moving right: " + isGoingRight);
        Debug.Log("Player is moving up: " + isGoingUp);
        RaycastHit hit;

        //if (
        //    Physics.Raycast(transform.position, transform.right, out hit, 5)
        //    || Physics.Raycast(transform.position, transform.right, out hit, -5)
        //    || Physics.Raycast(transform.position, transform.forward, out hit, 5)
        //    || Physics.Raycast(transform.position, transform.right, out hit, -5)
        //    )
        //    {
        //        if (hit.collider.CompareTag("Obstacle"))
        //        {
        //            if(hit.transform.position.z - transform.position.z <= 5 || hit.transform.position.z - transform.position.z >= -5)
        //            {
                        
        //            }   
        //        }
        //    }
        //else
        //    {
            if (zSteps > 0)
            {
                Debug.Log("Move Player on Z axis");
                Debug.Log("Goal z position: " + playerPosition.z + 5);
                if (isGoingUp ? transform.position.z <= playerPosition.z + 5 : transform.position.z >= playerPosition.z - 5)
                {
                    Debug.Log("Translating on Z axis");
                    transform.Translate(Vector3.forward * Time.deltaTime * (isGoingUp ? 1 : -1) * speed);
                    
                } else
                {
                    zSteps--;
                }
            } else if (xSteps > 0) {
                Debug.Log("Move Player on X Axis");
                Debug.Log("Goal x position: " + playerPosition.x + 5);
                Debug.Log("xSteps credit : " + xSteps);
                if (isGoingRight ? transform.position.x <= playerPosition.x + 5: transform.position.x >= playerPosition.x - 5)
                {
                    Debug.Log("Translating on X axis");
                    transform.Translate(Vector3.right * Time.deltaTime * (isGoingRight ? 1 : -1) * speed);
                    
                } else
                {
                    xSteps--;
                }
            }

    }
    

}

