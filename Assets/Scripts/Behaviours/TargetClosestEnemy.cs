using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetClosestEnemy : MonoBehaviour
{
    [SerializeField] private PlayerAttackController player;
    [SerializeField] private List<int> enemiesID = new List<int>();
    
    //this script is attached to a circle with a collider that handles when an enemy enters in the attack range

    //when an enemy enters the attack range
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag != "Enemy") return;
       if(enemiesID.Count == 0){
            enemiesID.Add(col.GetComponent<Enemy>().spawnId);
            player.FirstEnemy(col.gameObject);
       }else{
        enemiesID.Add(col.GetComponent<Enemy>().spawnId);
       }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag != "Enemy") return;
        
        if(enemiesID.Count > 1){
        if(Vector2.Distance(player.transform.position, other.transform.position) < player.distance && other.GetComponent<Enemy>().spawnId != player.target.GetComponent
            <Enemy>().spawnId){
            player.ChangeTarget(other.gameObject);
        }
        }
    }
    
    //when an enemy exits the attack range
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag != "Enemy") return;

        if(enemiesID.Count == 1 && enemiesID[0] == other.GetComponent<Enemy>().spawnId){
            
            player.LastEnemy();
        }

        enemiesID.Remove(other.GetComponent<Enemy>().spawnId);
    }
}
