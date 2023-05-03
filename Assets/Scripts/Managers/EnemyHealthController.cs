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
    public int maxHealth = 100;// TODO: this needs to be different for each kind of enemy
    

    // this function sets the health bar initially full
    public void SetupHealthBar()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("MorningStar"))
        {
            int damage = (int)col.rigidbody.angularVelocity;
            if (damage < 0) damage *= -1;
            print("damage: " + damage.ToString());
            health -= damage;
            UpdateHealthBar();
        }

        if (col.gameObject.CompareTag("Mace"))
        {
            int damage = (int)col.rigidbody.angularVelocity;
            if (damage < 0) damage *= -1;
            print("damage: " + damage.ToString());
            health -= damage;
            UpdateHealthBar();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        CheckDeath();
    }

}
