using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : HealthController
{
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = player.runStats.health; // initialize player health based on the stats
        health = maxHealth;
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
