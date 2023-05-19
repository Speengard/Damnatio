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
    
        Debug.Log("Player initial health: " + maxHealth);
    }

    override public bool CheckDeath() {
        // TODO: Game over
        return false;
    }

    // make the health bar appear
    public void EnableHealthBar() {
        healthSlider.gameObject.SetActive(true);
    }

    public override void TakeDamage(int damage)
    {
        print("arrived here");
        GameManager.Instance.followPlayer.ShakeCamera();
        base.TakeDamage(damage);
    }

}
