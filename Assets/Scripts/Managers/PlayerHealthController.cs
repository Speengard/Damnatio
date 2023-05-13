using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : HealthController
{
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GameObject.Find("Canvas").GetComponentInChildren<Slider>();

        maxHealth = GameManager.Instance.playerStats.health;
        health = PlayerPrefs.GetInt("PlayerHealth", maxHealth); // Get the player's health value from PlayerPrefs
    
        SetupHealthBar(maxHealth);
        Debug.Log("Player initial health: " + maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override protected void CheckDeath() {
        // TODO: Game over
    }

}
