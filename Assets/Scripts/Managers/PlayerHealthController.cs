using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : HealthController
{
    void Start()
    {
        maxHealth = GameManager.Instance.playerStats.health;
        health = PlayerPrefs.GetInt("PlayerHealth", maxHealth); // Get the player's health value from PlayerPrefs
    
        SetupHealthBar(maxHealth);
        Debug.Log("Player initial health: " + maxHealth);

        // in start scene the health bar shouldn't appear
        healthSlider.gameObject.SetActive(false);
    }

    override protected void CheckDeath() {
        // TODO: Game over
    }

    // make the health bar appear
    public void EnableHealthBar() {
        healthSlider.gameObject.SetActive(true);
    }

}
