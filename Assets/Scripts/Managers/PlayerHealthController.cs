using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [SerializeField] public int health;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = GetComponent<Player>().runStats.health; // initialize player health based on the stats
        health = maxHealth;
        Debug.Log("Player initial health: " + maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
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
	}

}
