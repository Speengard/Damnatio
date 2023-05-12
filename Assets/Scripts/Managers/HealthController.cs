using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthController : MonoBehaviour
{
    [SerializeField] protected Slider healthSlider;
    public int health; // current health
    static public int maxHealth;

    protected abstract void CheckDeath();
    
    // this function sets the health bar initially full
    public void SetupHealthBar(int value)
    {
        maxHealth = value;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        UpdateHealthBar(health);
    }

    public void UpdateHealthBar(int value) {
        // updates how much the bar is filled
        
        // healthSlider.value = health;
        healthSlider.value = value;
        CheckDeath();
    }

    public void AddHealth(int value) {
		health += value;

		// check if the player is dead
		if (health <= 0) {
			Debug.Log("Game Over!"); // TODO: Game over scene
			health = 0;
		} else if (health > maxHealth) {
            health = maxHealth;
        }

        // TODO: call function that updates health bar
		Debug.Log("Health Bar = " + health);
        UpdateHealthBar(health);
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar(health);
    }

}
