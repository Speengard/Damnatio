using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                if(!player.hasEnemy){
                    player.HasEnemy(col.gameObject);
                }
            }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject == player.target) return;
        
        if(other.gameObject.CompareTag("Enemy")){
            if(Vector2.Distance(player.transform.position,other.transform.position) < player.distance){
                player.ChangeTarget(other.gameObject);
            }
        }
    }
    
    //when an enemy exits the attack range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!player.hasEnemy) return;
        
        if (other.gameObject.CompareTag("Enemy") && player.hasEnemy)
        {
            //lost enemy
            player.LostEnemy();
        }
    }
    
}
