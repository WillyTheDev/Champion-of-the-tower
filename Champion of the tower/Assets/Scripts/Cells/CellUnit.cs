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
        

        if (PlayerController.playerIsAttacking && TurnSystem.isPlayerTurn)
        {
            Vector3 distanceWithPlayer = transform.position - PlayerMovement.playerPosition;

            if (Math.Abs(distanceWithPlayer.x) + Math.Abs(distanceWithPlayer.z) <= PlayerController.selectedSpell.maxDistance && Math.Abs(distanceWithPlayer.x) + Math.Abs(distanceWithPlayer.z) >= PlayerController.selectedSpell.minDistance && transform.position != PlayerMovement.playerPosition - new Vector3(0, 1.9f, 0))
            {
                cellMaterial.color = Color.blue;
            } else
            {
                cellMaterial.color = new Color(0.7379405f, 0.9589565f, 0.9716981f, 0.2f);
            }

            if (CombatSystem.selectedAttackCells.Count > 0)
            {
                foreach (Vector3 cell in CombatSystem.selectedAttackCells)
                {
                    if (cell == transform.position)
                    {
                        cellMaterial.color = Color.red;
                    }
                }
            }
            



        }


        else if (TurnSystem.isPlayerTurn && CellData.cellPath.Count > 0)
        {
            Debug.Log("CellPath Count = " + CellData.cellPath.Count);
            if (CellData.cellPath.Contains(new Vector3(transform.position.x, 2, transform.position.z)))
            {
                Debug.Log("Cell is on Path");
                cellMaterial.color = Color.green;
            } else
            {
                cellMaterial.color = new Color(0.7379405f, 0.9589565f, 0.9716981f, 0.2f);
            }
        } else
        {
            cellMaterial.color = new Color(0.7379405f, 0.9589565f, 0.9716981f, 0.2f);
        }

        

    }

    private void OnMouseOver()
    {
        if (canBeTargeted && TurnSystem.isPlayerTurn && !PlayerMovement.playerIsMoving && !PlayerController.playerIsAttacking)
        {
            Vector3 distanceWithPlayer = transform.position - PlayerMovement.playerPosition;
            if (Math.Abs(distanceWithPlayer.x) + Math.Abs(distanceWithPlayer.z) <= PlayerData.playerMovementPoint * 5 && transform.position != PlayerMovement.playerPosition - new Vector3(0, 1.9f, 0))
            {
                cellMaterial.color = Color.green;
                PlayerMovement.targetCell = gameObject;
            }
        }
        else if (PlayerController.selectedSpell != null)
            {
            Vector3 distanceWithPlayer = transform.position - PlayerMovement.playerPosition;
            if (Math.Abs(distanceWithPlayer.x) + Math.Abs(distanceWithPlayer.z) <= PlayerController.selectedSpell.maxDistance && Math.Abs(distanceWithPlayer.x) + Math.Abs(distanceWithPlayer.z) >= PlayerController.selectedSpell.minDistance && transform.position != PlayerMovement.playerPosition - new Vector3(0, 1.9f, 0))
            {
                Vector3 cellPosition = transform.position;
                switch (PlayerController.selectedSpell.pattern)
                {
                    case "xLine":
                        CombatSystem.selectedAttackCells = new List<Vector3> { cellPosition, cellPosition + Vector3.right * 5, cellPosition + Vector3.left * 5 };
                        break;
                    case "zLine":
                        CombatSystem.selectedAttackCells = new List<Vector3> { cellPosition, cellPosition + Vector3.forward * 5, cellPosition + Vector3.back * 5 };
                        break;
                    case "Cross":
                        CombatSystem.selectedAttackCells = new List<Vector3> { cellPosition, cellPosition + Vector3.forward * 5, cellPosition + Vector3.back * 5, cellPosition + Vector3.right * 5, cellPosition + Vector3.left * 5 };
                        break;
                    default:
                        CombatSystem.selectedAttackCells = new List<Vector3> { cellPosition };
                        return;
                }
            }
            
            
        }

    }

    private void OnMouseEnter()
    {
        if (canBeTargeted && TurnSystem.isPlayerTurn && !PlayerMovement.playerIsMoving && !PlayerController.playerIsAttacking)
        {
            Vector3 distanceWithPlayer = transform.position - PlayerMovement.playerPosition;
            if (Math.Abs(distanceWithPlayer.x) + Math.Abs(distanceWithPlayer.z) <= PlayerData.playerMovementPoint * 5 && transform.position != PlayerMovement.playerPosition - new Vector3(0, 1.9f, 0))
            {
                CellData.cellPath = PlayerMovement.PathFindingPlayer(transform.position);
                PlayerMovement.targetCell = gameObject;
            }
        }
        
        
    }

    private void OnMouseExit()
    {
        cellMaterial.color = new Color(0.7379405f, 0.9589565f, 0.9716981f, 0.2f);
        CellData.cellPath = new List<Vector3>();
        PlayerMovement.targetCell = null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            canBeTargeted = false;
        }
    }


}
