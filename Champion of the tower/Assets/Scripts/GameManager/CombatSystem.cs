using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class CombatSystem
{

    public static bool playerIsAttacking = false;
    public static Spells.Spell selectedSpell;
    public static GameObject selectedEnemy = new GameObject();
    public static GameObject selectedPlayer = GameObject.FindGameObjectWithTag("Player");
    // Start is called before the first frame update
    
     public static void PlayerAttack(Spells.Spell spell, GameObject enemy)
    {
        EnemyData enemyData = enemy.GetComponent<EnemyData>();
        enemyData.health -= spell.power;
        PlayerData.playerActionPoint -= spell.actionPointRequired;
    }

    public static void EnnemyAttack(Spells.Spell spell, GameObject player, EnemyData enemyData)
    {
        Debug.Log("Enemy is Attacking player !");
        PlayerData.health -= spell.power;
        enemyData.enemyActionPoint -= spell.actionPointRequired;
    }

    public static void enableAttackState()
    {
        playerIsAttacking = !playerIsAttacking;
        selectedSpell = Spells.testAttack;
    }

}
