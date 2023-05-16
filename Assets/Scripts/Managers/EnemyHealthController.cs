using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : HealthController
{
    //this script is used to manage the health of the enemy;
    //this script also updates the HUD when damage is taken.

    // this function destroys the enemy when the health goes below 0
    override protected void CheckDeath() {
        if (health <= 0)
        {
            GameManager.Instance.enemies.Remove(gameObject.GetComponent<Enemy>());
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        CheckDeath();
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) health = 0;
        UpdateHealthBar(health);      
    }

}
