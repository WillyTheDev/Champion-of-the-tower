using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    static public bool isPlayerTurn;
    public int turnLength = 30;
    public float timeLeft;
    private GameObject[] enemies;
    private bool turnSystemIsOn = true;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        timeLeft = turnLength;
        StartCoroutine(TurnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (turnSystemIsOn)
        {
            timeLeft -= 1 * Time.deltaTime;
        }
    }


    

    IEnumerator TurnLoop()
    {
        foreach (GameObject enemy in enemies)
        {
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.enemyMovementPoint = 4;
            enemyMovement.isEnemyTurn = true;
            yield return new WaitForSeconds(turnLength);
            timeLeft = turnLength;
            enemyMovement.isEnemyTurn = false;
        }
        isPlayerTurn = true;
        PlayerMovement.playerMovementPoint = 3;
        yield return new WaitForSeconds(turnLength);
        isPlayerTurn = false;

        if (turnSystemIsOn)
        {
            StartCoroutine(TurnLoop());
        }
    }

}
