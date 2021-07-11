using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CellUnit : MonoBehaviour
{
    // Start is called before the first frame update
    private Material cellMaterial;
    private bool canBeTargeted = true;
    void Start()
    {
        cellMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.playerIsAttacking)
        {
            if(Math.Abs(transform.position.x) <= PlayerMovement.playerPosition.x + 5 && Math.Abs(transform.position.y) <= PlayerMovement.playerPosition.y + 5)
            {
                cellMaterial.color = Color.red;
            }
        }
        if (TurnSystem.isPlayerTurn && CellData.cellPath.Count > 0)
        {
            if (CellData.cellPath.Contains(new Vector3(transform.position.x, 2, transform.position.z)))
            {
                Debug.Log("Cell is on Path");
                cellMaterial.color = Color.green;
            } else
            {
                cellMaterial.color = new Color(0.7379405f, 0.9589565f, 0.9716981f, 0.2f);
            }
        }
        if (PlayerMovement.playerIsMoving)
        {
            cellMaterial.color = new Color(0.7379405f, 0.9589565f, 0.9716981f, 0.2f);
        }
    }

    private void OnMouseOver()
    {
        if (canBeTargeted && TurnSystem.isPlayerTurn && !PlayerMovement.playerIsMoving)
        { 
            Vector3 distanceWithPlayer = transform.position - PlayerMovement.playerPosition;
            if (Math.Abs(distanceWithPlayer.x) + Math.Abs(distanceWithPlayer.z) <= PlayerMovement.playerMovementPoint * 5 && transform.position != PlayerMovement.playerPosition - new Vector3(0, 1.9f, 0))
            {
                cellMaterial.color = Color.green;
                PlayerMovement.targetCell = gameObject;
                if (PlayerMovement.IsThereAnObstacleOnPath())
                {
                    cellMaterial.color = Color.red;
                    PlayerMovement.targetCell = null;
                }
            }
        } 
    }
    private void OnMouseEnter()
    {
        if (canBeTargeted && TurnSystem.isPlayerTurn && !PlayerMovement.playerIsMoving)
        {
            Vector3 distanceWithPlayer = transform.position - PlayerMovement.playerPosition;
            if (Math.Abs(distanceWithPlayer.x) + Math.Abs(distanceWithPlayer.z) <= PlayerMovement.playerMovementPoint * 5 && transform.position != PlayerMovement.playerPosition - new Vector3(0, 1.9f, 0))
            {
                CellData.cellPath = PlayerMovement.PathFindingPlayer(transform.position);
                PlayerMovement.targetCell = gameObject;
            }
        }
        
    }

    private void OnMouseExit()
    {
        cellMaterial.color = new Color(0.7379405f, 0.9589565f, 0.9716981f, 0.2f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            canBeTargeted = false;
        }
    }


}
