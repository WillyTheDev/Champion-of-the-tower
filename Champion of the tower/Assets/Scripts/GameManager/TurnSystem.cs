using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    static public bool isPlayerTurn;
    public int turnLength = 30;
    public float timeLeft;
    private GameObject[] enemies;
    public bool turnSystemIsOn;
    private Slider timeSlider;
    public GameObject battleCanvas;
    public GameObject chest;
    public GameObject preparationCanvas;
    public GameObject camera;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera.GetComponent<CameraController>().objectToFollow = player;
        Debug.Log("TurnSystem Started");
        isPlayerTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && turnSystemIsOn)
        {
            turnSystemIsOn = false;
            battleCanvas.GetComponent<Canvas>().enabled = false;
            isPlayerTurn = true;
            Instantiate(chest, transform.position, transform.rotation);
        }


        if (turnSystemIsOn)
        {
            timeLeft -= 1 * Time.deltaTime;
            timeSlider.value = timeLeft;
        } else
        {
            PlayerData.playerMovementPoint = 30;
        }
    }

    public void InitializingCombat()
    {
        turnSystemIsOn = true;
        battleCanvas.GetComponent<Canvas>().enabled = true;
        preparationCanvas.GetComponent<Canvas>().enabled = false;
        PlayerData.playerMovementPoint = 3;
        isPlayerTurn = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        timeLeft = turnLength;
        timeSlider = GameObject.FindGameObjectWithTag("TimeSlider").GetComponent<Slider>();
        timeSlider.maxValue = turnLength;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float enemyInitiative = 0;
        foreach(GameObject enemy in enemies)
        {
            enemyInitiative += enemy.GetComponent<EnemyData>().initiative;
        }
        enemyInitiative = enemyInitiative / enemies.Length;

        StartCoroutine(TurnLoop(enemyInitiative, PlayerData.initiative));
    }


    IEnumerator TurnLoop(float enemyInit, float playerInit)
    {
        if(enemyInit > playerInit)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                camera.GetComponent<CameraController>().objectToFollow = enemy;
                EnemyData enemyData = enemy.GetComponent<EnemyData>();
                enemyData.enemyMovementPoint = 4;
                enemyData.enemyActionPoint = 4;
                enemyData.isEnemyTurn = true;
                yield return new WaitForSeconds(turnLength);
                timeLeft = turnLength;
                enemyData.isEnemyTurn = false;
            }
            isPlayerTurn = true;
            camera.GetComponent<CameraController>().objectToFollow = player;
            PlayerData.playerMovementPoint = 3;
            PlayerData.playerActionPoint = 6;
            yield return new WaitForSeconds(turnLength);
            isPlayerTurn = false;
            timeLeft = turnLength;
        } else
        {
            isPlayerTurn = true;
            camera.GetComponent<CameraController>().objectToFollow = player;
            PlayerData.playerMovementPoint = 3;
            PlayerData.playerActionPoint = 6;
            yield return new WaitForSeconds(turnLength);
            isPlayerTurn = false;
            timeLeft = turnLength;

            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                camera.GetComponent<CameraController>().objectToFollow = enemy;
                EnemyData enemyData = enemy.GetComponent<EnemyData>();
                enemyData.enemyMovementPoint = 4;
                enemyData.enemyActionPoint = 4;
                enemyData.isEnemyTurn = true;
                yield return new WaitForSeconds(turnLength);
                timeLeft = turnLength;
                enemyData.isEnemyTurn = false;
            }
        }
        if (turnSystemIsOn)
        {
            StartCoroutine(TurnLoop(enemyInit, playerInit));
        }
    }

}
