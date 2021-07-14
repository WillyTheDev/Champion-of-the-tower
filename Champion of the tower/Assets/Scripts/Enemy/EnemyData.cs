using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    // Start is called before the first frame update
    public int initiative = 100;
    private int m_health = 60;
    public bool isEnemyTurn = false;
    public int enemyActionPoint = 4;
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

   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        CombatSystem.selectedEnemy = gameObject;
        Debug.Log("Enemy Selected = " + gameObject.name);
    }

    private void OnMouseExit()
    {
        CombatSystem.selectedEnemy = new GameObject();
    }
}
