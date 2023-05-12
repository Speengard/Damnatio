using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : HealthController
{
    //[SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        healthSlider = GameObject.Find("Canvas").GetComponentInChildren<Slider>();

        maxHealth = GameManager.Instance.playerStats.health;
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
