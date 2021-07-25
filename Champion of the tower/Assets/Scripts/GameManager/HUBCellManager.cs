using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUBCellManager : MonoBehaviour
{
    public GameObject cell;
    public GameObject player;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateAllCells(10,10);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x == door.transform.position.x && player.transform.position.z == door.transform.position.z)
        {
            PlayerMovement.playerIsMoving = false;
            Debug.Log("Loading Next Scene");
            SceneManager.LoadScene("MainScene");
        }
    }

    private void InstantiateAllCells(int rows, int columns)
    {
        for (int column = 0; column <= columns; column++)
        {
            for (int row = 0; row <= rows; row++)
            {
                Instantiate(cell, transform.position + new Vector3(-25 + (row * 5), 0.1f, -25 + (column * 5)), transform.rotation);
            }
        }

    }
}
