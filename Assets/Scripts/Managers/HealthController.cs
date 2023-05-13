using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthController : MonoBehaviour
{
    [SerializeField] protected Slider healthSlider;
    public int health; // current health
    public int maxHealth;

    protected abstract void CheckDeath();
    
    // this function sets the health bar initially full
    public void SetupHealthBar(int value)
    {
        maxHealth = value;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;

        if (gameObject.CompareTag("Player")) {
            Debug.Log("Setting up health bar with value: " + value);
        }

        UpdateHealthBar(health);
    }

    public void UpdateHealthBar(int value) {
        // updates how much the bar is filled
        healthSlider.value = value;
        CheckDeath();
    }

    public void AddHealth(int value) {
		health += value;

		if (health > maxHealth) health = maxHealth;

		Debug.Log("Health Bar = " + health);
        PlayerPrefs.SetInt("PlayerHealth", health);

        UpdateHealthBar(health);
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)    health = 0;
        PlayerPrefs.SetInt("PlayerHealth", health);
        UpdateHealthBar(health);
    }

}
