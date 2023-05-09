using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    //this script is used to manage the health of the enemy; since the health is lowered when contact happens with weapon,
    //some functions of collisions will be handled here.
    //this script also updates the HUD when damage is taken.
    
    [SerializeField] private Slider healthSlider;
    public int health; // current health
    public int maxHealth;// TODO: this needs to be different for each kind of enemy
    

    // this function sets the health bar initially full
    public void SetupHealthBar(int value)
    {
        maxHealth = value;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        UpdateHealthBar();
    }

    public void UpdateHealthBar() {
        
        // updates how much the bar is filled
        healthSlider.value = health;
        CheckDeath();
    }

    // this function destroys the enemy when the health goes below 0
    public void CheckDeath() {
        if (health <= 0)
        {
            GameManager.Instance.enemies.Remove(gameObject.GetComponent<Enemy>());
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar();
    }

    private void OnCollisionExit(Collision other)
    {
        CheckDeath();
    }

}
