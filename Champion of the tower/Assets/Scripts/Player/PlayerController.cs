using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        if (CombatSystem.playerIsAttacking && CombatSystem.selectedEnemy != null && Input.GetMouseButtonDown(0))
        {
            CombatSystem.PlayerAttack(Spells.testAttack, CombatSystem.selectedEnemy);
        }
    }
}
