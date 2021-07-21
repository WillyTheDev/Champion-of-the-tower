using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject cell;
    public GameObject obstacle;
    public GameObject player;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateAllCells(rows: 10, columns: 10);
        InstantiateObstacles();
        InstantiatePlayer();
        InstantiateEnemy();
    }

    // Update is called once per frame
    void Update()
    {

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

    private void InstantiateObstacles()
    {
        for (int i = 0; i <= Random.Range(6, 12); i++)
        {
            Debug.Log("Instantiating Obstacle");
            Instantiate(obstacle, new Vector3(Random.Range(-5, 5) * 5, 2f, Random.Range(-5, 5) * 5), transform.rotation);
        }
    }

    private void InstantiatePlayer()
    {
        Instantiate(player, transform.position, transform.rotation);
    }

    private void InstantiateEnemy()
    {
        for (int i = 0; i <= Random.Range(1, 3); i++)
        {
            Debug.Log("Instantiating Obstacle");
            Instantiate(Enemy,transform.position, transform.rotation);
        }
    }
}
