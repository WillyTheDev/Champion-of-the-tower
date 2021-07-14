using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    private EnemyData enemyData;
    private EnemyMovement enemyMovement;
    private bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        enemyData = GetComponent<EnemyData>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyData.isEnemyTurn && enemyMovement.enemyMovementPoint == 0 && !canAttack && enemyData.enemyActionPoint > 0)
        {
            checkEnemyContact();
        }

        if (canAttack)
        {
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 5))
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
