using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyData : MonoBehaviour
{
    // Start is called before the first frame update
    public int initiative = 100;
    private int m_health = 60;
    public string enemyName = "EnemyName";
    public bool isEnemyTurn = false;
    public int enemyActionPoint = 4;
    public int enemyMovementPoint;
    public int health
    {
        get { return m_health; }
        set {
            if(value >= 0)
            {
                m_health = value;
            } else
            {
                m_health = 0;
            }
            
        }
    }

    private bool enemyIsSelected = false;
    public TextMeshProUGUI enemyTextName;
    public TextMeshProUGUI enemyTextHealth;
    public TextMeshProUGUI enemyTextActionPoint;
    public TextMeshProUGUI enemyTextMovementPoint;
   
    void Start()
    {
        enemyTextName = GameObject.FindGameObjectWithTag("EnemyTextName").GetComponent<TextMeshProUGUI>();
        enemyTextHealth = GameObject.FindGameObjectWithTag("EnemyTextHealth").GetComponent<TextMeshProUGUI>();
        enemyTextActionPoint = GameObject.FindGameObjectWithTag("EnemyActionPoint").GetComponent<TextMeshProUGUI>();
        enemyTextMovementPoint = GameObject.FindGameObjectWithTag("EnemyMovementPoint").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyIsSelected)
        {
            enemyTextName.text = enemyName;
            enemyTextHealth.text = "Health : " + health.ToString();
            enemyTextActionPoint.text = "PA : " + enemyActionPoint.ToString();
            enemyTextMovementPoint.text = "PM : " + enemyMovementPoint.ToString();
        }

        if(health == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseOver()
    {
            CombatSystem.selectedEnemy = gameObject;
            enemyIsSelected = true;
            Debug.Log("Enemy Selected = " + gameObject.name);
    }

    private void OnMouseExit()
    {
        enemyIsSelected = false;
        CombatSystem.selectedEnemy = new GameObject();
    }
}
