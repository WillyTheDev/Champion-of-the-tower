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
        if (playerIsAttacking && Input.GetMouseButtonDown(0) && PlayerData.playerActionPoint > 0 && TurnSystem.isPlayerTurn)
        {
            CombatSystem.PlayerAttack(selectedSpell, CombatSystem.selectedAttackCells);
            playerIsAttacking = !playerIsAttacking;
        }

        if(playerIsAttacking && Input.GetMouseButtonDown(1)){
            playerIsAttacking = !playerIsAttacking;
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
