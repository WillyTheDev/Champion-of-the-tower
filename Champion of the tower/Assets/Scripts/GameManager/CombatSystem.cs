using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{

    
    
    public static GameObject selectedEnemy = new GameObject();
    public static List<Vector3> selectedAttackCells = new List<Vector3>();
    public static GameObject selectedPlayer = GameObject.FindGameObjectWithTag("Player");
    // Start is called before the first frame update
    
     public static void PlayerAttack(Spells.Spell spell, List<Vector3> selectedCells)
    {
        selectedPlayer.GetComponent<PlayerMovement>().RotatePlayerBasedOnTargetCell(selectedCells[0]);
        GameObject spellVFX = GameObject.Find(spell.spellPrefabsName);
        GameObject[] potentialsEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        
        Instantiate(spellVFX, selectedPlayer.transform.position, selectedPlayer.transform.rotation);

        foreach (GameObject enemy in potentialsEnemy)
            {
                foreach(Vector3 cell in selectedCells)
                {
                    if (enemy.transform.position == cell + new Vector3(0, 1.9f, 0))
                    {
                    EnemyData enemyData = enemy.GetComponent<EnemyData>();
                    enemyData.health -= spell.power;
                    }
                }
            }

        PlayerData.playerActionPoint -= spell.actionPointRequired;
    }

    public static void EnnemyAttack(Spells.Spell spell, GameObject player, EnemyData enemyData)
    {
        Debug.Log("Enemy is Attacking player !");
        PlayerData.health -= spell.power;
        enemyData.enemyActionPoint -= spell.actionPointRequired;
    }

    

}
