using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetClosestEnemy : MonoBehaviour
{
    [SerializeField] private PlayerAttackController player;
    [SerializeField] private List<GameObject> enemiesID = new List<GameObject>();
    
    //this script is attached to a circle with a collider that handles when an enemy enters in the attack range

    //when an enemy enters the attack range
    private void OnTriggerEnter2D(Collider2D col)
    {

        if(col.tag != "Enemy") return;
       if(enemiesID.Count == 0){
            enemiesID.Add(col.gameObject);
            player.FirstEnemy(col.gameObject);
       }else{
        enemiesID.Add(col.gameObject);
       }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag != "Enemy") return;
        
        if(enemiesID.Count >= 1){

            
        if(Vector2.Distance(player.transform.position, other.transform.position) < player.distance){
            player.ChangeTarget(other.gameObject);
        }else if(player.target == null){
            player.ChangeTarget(other.gameObject);
        }

        }
    }
    
    //when an enemy exits the attack range
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag != "Enemy") return;

        if(enemiesID.Count == 1 && enemiesID[0] == other.gameObject){
            player.LastEnemy();
        }

        enemiesID.Remove(other.gameObject);
    }
}
