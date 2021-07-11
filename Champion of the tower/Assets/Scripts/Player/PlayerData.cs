using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static int initiative = 110;
    public static bool playerIsAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableAttackState()
    {
        playerIsAttacking = !playerIsAttacking;
    }

    public void disableAttackState()
    {
        playerIsAttacking = false;
    }
}
