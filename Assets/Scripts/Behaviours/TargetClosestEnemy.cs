using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetClosestEnemy : MonoBehaviour
{
    [SerializeField] private PlayerAttackController player;
    //this script is attached to a circle with a collider that handles when an enemy enters in the attack range
    
    //when an enemy enters the attack range
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            {
                if(!player.hasEnemy)
                {
                    //this is the first enemy entered in the attack range
                    //has enemy
                    player.HasEnemy(col.gameObject);
                }
                else
                {
                    if (Vector2.Distance(player.transform.position, col.transform.position) < player.distance)
                    {
                        //if this is not the first enemy in the range, then if it is the closest, it is the current enemy
                        //has enemy
                        player.HasEnemy(col.gameObject);
                        
                    }
                }
            }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Vector2.Distance(player.transform.position, other.transform.position) < player.distance)
        {
            //if this is not the first enemy in the range, then if it is the closest, it is the current enemy
            //has enemy
            player.HasEnemy(other.gameObject);
        }
    }
    
    //when an enemy exits the attack range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (player.hasEnemy == false) return;
        if (other.gameObject.CompareTag("Enemy") && player.hasEnemy)
        {
            //lost enemy
            player.LostEnemy();
        }
    }
}
