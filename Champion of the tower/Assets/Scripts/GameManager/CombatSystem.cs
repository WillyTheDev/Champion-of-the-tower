using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class CombatSystem
{

    public static bool playerIsAttacking = false;
    public static Spells.Spell selectedSpell;
    public static GameObject selectedEnemy = new GameObject();
    // Start is called before the first frame update
    
     public static void Attack(Spells.Spell spell, GameObject enemy)
    {
        EnemyData enemyData = enemy.GetComponent<EnemyData>();
        enemyData.health -= spell.power;
    }

    public static void enableAttackState()
    {
        playerIsAttacking = !playerIsAttacking;
        selectedSpell = Spells.testAttack;
    }

}
