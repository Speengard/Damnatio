using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetClosestEnemy : MonoBehaviour
{
    [SerializeField] private PlayerAttackController player;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if(!player.hasEnemy)
            {
                player.target = col.gameObject;
                player.hasEnemy = true;
            }
            else
            {
                if (Vector2.Distance(player.transform.position, col.transform.position) < player.distance)
                {
                    player.target = col.gameObject; 
                    player.hasEnemy = true;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Vector2.Distance(player.transform.position, other.transform.position) < player.distance)
            {
                player.target = other.gameObject;

            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && player.hasEnemy)
        {
            player.target = null;
            player.hasEnemy = false;
        }
    }
}
