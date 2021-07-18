using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    public static bool playerIsAttacking = false;
    public static Spells.Spell selectedSpell;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (playerIsAttacking && CombatSystem.selectedEnemy != null && Input.GetMouseButtonDown(0) && PlayerData.playerActionPoint > 0)
        {
            CombatSystem.PlayerAttack(Spells.testAttack, CombatSystem.selectedEnemy);
        }
    }

    public static void enableTestAttackState()
    {
        playerIsAttacking = !playerIsAttacking;
        selectedSpell = Spells.testAttack;
    }

    public static void enableTestDistanceAttackState()
    {
        playerIsAttacking = !playerIsAttacking;
        selectedSpell = Spells.testDistanceAttack;
    }
}
