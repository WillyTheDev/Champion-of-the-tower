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
    private static int m_playerMovementDistance = 20;
    public static int playerMovementDistance {
        get { return m_playerMovementDistance; }
        set {
            if (value % 5 == 0)
            {
                m_playerMovementDistance = value;
            }
            else
            {
                m_playerMovementDistance = 20;
                Debug.Log("Value can't be divised by 5!");
            }
        }
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
        if (Input.GetMouseButtonDown(0) && targetCell != null && !playerIsMoving)
        {
            selectedCell = targetCell;
            Debug.Log(selectedCell.transform.position);
            // Offsets are used to calculate how many cells there is between player position and selectedCells
            // Offsets are used to determine if the movement will be negative or positive.
            xOffset = Convert.ToInt16(selectedCell.transform.position.x - transform.position.x);
            zOffset = Convert.ToInt16(selectedCell.transform.position.z - transform.position.z);
            playerIsMoving = true;
            
        }
        if (playerIsMoving)
        {
            MovePlayerToSelectedCell(xOffset, zOffset);
        }
    }

   
    private void MovePlayerToSelectedCell(int xOffset, int zOffset)
    {
        //Steps will be use in further development
        int zStep = Math.Abs(zOffset / 5);
        int xStep = Math.Abs(xOffset / 5);

        //First we will move forward or downward and then right or left.
        if(zOffset < 0 ? transform.position.z >= selectedCell.transform.position.z : transform.position.z <= selectedCell.transform.position.z)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * (zOffset < 0 ? -1 : 1) * speed);
        } else if (xOffset < 0 ? transform.position.x >= selectedCell.transform.position.x : transform.position.x <= selectedCell.transform.position.x)
        {
            transform.Translate(Vector3.right * Time.deltaTime * (xOffset < 0 ? -1 : 1) * speed);
        } else
        {
            // Center the Player position in the Cell and Stopping the movement.
            transform.position = selectedCell.transform.position + new Vector3(0, 2, 0);
            playerPosition = transform.position;
            playerIsMoving = false;
        }
    }
    

}

