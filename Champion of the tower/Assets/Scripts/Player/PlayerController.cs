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
    private static bool playerIsMoving;
    private int xOffset;
    private int zOffset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && targetCell != null)
        {
            selectedCell = targetCell;
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
        Debug.Log("x Offset :" + xOffset + ", z offset :" + zOffset);
        int zStep = Math.Abs(zOffset / 5);
        int xStep = Math.Abs(xOffset / 5);
        if(zOffset < 0 ? transform.position.z >= selectedCell.transform.position.z : transform.position.z <= selectedCell.transform.position.z)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * (zOffset < 0 ? -1 : 1) * speed);
        } else if (xOffset < 0 ? transform.position.x >= selectedCell.transform.position.x : transform.position.x <= selectedCell.transform.position.x)
        {
            transform.Translate(Vector3.right * Time.deltaTime * (xOffset < 0 ? -1 : 1) * speed);
        } else
        {
            transform.position = selectedCell.transform.position + new Vector3(0, 2, 0);
            playerIsMoving = false;
        }

    }
}
