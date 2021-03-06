using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    private EnemyData enemyData;
    private EnemyMovement enemyMovement;
    public bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        enemyData = GetComponent<EnemyData>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyData.isEnemyTurn && enemyData.enemyActionPoint > 0 && enemyMovement.enemyIsMoving == false)
        {
            print("Check Contact");
            checkEnemyContact();
        }

        if (canAttack)
        {
            print("Player Contact, enemy is attacking !");
            CombatSystem.EnnemyAttack(Spells.testAttack, CombatSystem.selectedPlayer, enemyData);
            canAttack = false;
        }

    }

    public void checkEnemyContact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 5))
        {
            if (hit.collider.CompareTag("Player"))
            {
                canAttack = true;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.back, out hit, 5))
        {
            if (hit.collider.CompareTag("Player"))
            {
                canAttack = true;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right, out hit, 5))
        {
            if (hit.collider.CompareTag("Player"))
            {
                canAttack = true;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.left, out hit, 5))
        {
            if (hit.collider.CompareTag("Player"))
            {
                canAttack = true;
            }
        }
    }

}
