using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    static public bool isPlayerTurn;
    public int turnLength = 30;
    public float timeLeft;
    private GameObject[] enemies;
    private bool turnSystemIsOn = true;
    private Slider timeSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        timeLeft = turnLength;
        timeSlider = GameObject.FindGameObjectWithTag("TimeSlider").GetComponent<Slider>();
        timeSlider.maxValue = turnLength;
        StartCoroutine(TurnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (turnSystemIsOn)
        {
            timeLeft -= 1 * Time.deltaTime;
            timeSlider.value = timeLeft;
        }
    }


    

    IEnumerator TurnLoop()
    {
        foreach (GameObject enemy in enemies)
        {
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            EnemyData enemyData = enemy.GetComponent<EnemyData>();
            enemyMovement.enemyMovementPoint = 4;
            enemyData.isEnemyTurn = true;
            yield return new WaitForSeconds(turnLength);
            timeLeft = turnLength;
            enemyData.isEnemyTurn = false;
        }
        isPlayerTurn = true;
        PlayerMovement.playerMovementPoint = 3;
        yield return new WaitForSeconds(turnLength);
        isPlayerTurn = false;
        timeLeft = turnLength;
        if (turnSystemIsOn)
        {
            StartCoroutine(TurnLoop());
        }
    }

}
