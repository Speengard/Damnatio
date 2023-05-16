using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthController : MonoBehaviour
{
    [SerializeField] public Slider healthSlider;
    public int health; // current health
    public int maxHealth;

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
        healthSlider.value = value;
        CheckDeath();
    }

    public void AddHealth(int value) {
		health += value;

		if (health > maxHealth) health = maxHealth;
        PlayerPrefs.SetInt("PlayerHealth", health);

        UpdateHealthBar(health);
	}

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0){
            
            health = 0;
            UpdateHealthBar(health);

        }else{

            //MARK: - Warning, this line below gets executed on every call of this function from a class that doesn't provide its own implementation of this function, so be careful about implementing this in a class that inherits from this class and doesn't want this line to be executed (ex: EnemyHealthController.cs)

            PlayerPrefs.SetInt("PlayerHealth", health);
            UpdateHealthBar(health);
            
        }
    }

}
