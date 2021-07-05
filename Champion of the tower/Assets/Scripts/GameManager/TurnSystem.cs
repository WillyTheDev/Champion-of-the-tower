using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    static public bool isPlayerTurn;
    static public bool isEnemyTurn;
    public int turnLength = 30;
    public float timeLeft;
    private GameObject enemy;
    private EnemyData enemyData;
    private bool turnSystemIsOn = true;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyData = enemy.GetComponent<EnemyData>();
        timeLeft = turnLength;
        CheckingInitiative();
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
        yield return new WaitForSeconds(turnLength);
        Debug.Log("Time Left = " + timeLeft);
        Debug.Log("Turn is Switching");
        isEnemyTurn = !isEnemyTurn;
        isPlayerTurn = !isPlayerTurn;
        timeLeft = turnLength;
        EnemyMovement.enemyMovementPoint = 1;
        if (turnSystemIsOn)
        {
            StartCoroutine(TurnLoop());
        }
    }

    private void CheckingInitiative()
    {
        if(enemyData.initiative > PlayerData.initiative)
        {
            isEnemyTurn = true;
            isPlayerTurn = false;
        } else
        {
            isPlayerTurn = true;
            isEnemyTurn = false;
        }
    }

}
