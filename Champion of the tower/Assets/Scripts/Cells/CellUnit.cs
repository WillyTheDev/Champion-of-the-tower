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

    }

    private void OnMouseEnter()
    {
        if (canBeTargeted)
        {
            Vector3 distanceWithPlayer = transform.position - PlayerController.playerPosition;
            if (Math.Abs(distanceWithPlayer.x) + Math.Abs(distanceWithPlayer.z) <= PlayerController.playerMovementDistance)
            {
                cellMaterial.color = Color.green;
                PlayerController.targetCell = gameObject;
                if (PlayerController.IsThereAnObstacleOnPath())
                {
                    cellMaterial.color = Color.red;
                    PlayerController.targetCell = null;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        cellMaterial.color = new Color(1, 0.4622955f, 0.25f, 0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            canBeTargeted = false;
            cellMaterial.color = Color.red;
        }
    }


}
