using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellManager : MonoBehaviour
{
    public GameObject cell;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateAllCells(rows: 10, columns: 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateAllCells(int rows, int columns)
    {
        for(int column = 0; column <=columns; column++)
        {
            for (int row = 0; row <= rows; row++)
            {
                Instantiate(cell, transform.position + new Vector3(-25 + (row * 5), 0.1f, -25 + (column * 5)), transform.rotation);
            }
        }
        
    }
}
