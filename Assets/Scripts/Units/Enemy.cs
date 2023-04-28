using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Enemy : MovingObject
{
    public int playerDamage; // damage that the player does on the enemy
    public int damage; // damage that the enemy does on the player
    public int health; // current health
    public int maxHealth = 100; // TODO: this needs to be different for each kind of enemy
    public Slider healthSlider;

    private Animator animator;
    public Transform target;
    private bool skipMove;
    private float speed = 1f;

    public AudioClip[] attackClips;

    protected override void Start()
    {
        //GameManager.Instance.AddEnemyToList(this);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        print(target);
        base.Start();

        SetupHealthBar();
        UpdateHealthBar(); // updates the value of the health bar and checks if the enemy is dead
    }

    private void Update()
    {

        if (Vector3.Distance(transform.position, target.position) > 0.3f)
        {
            //move if distance from target is greater than 1
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x,target.position.y), speed * Time.deltaTime);
        }


    }

    // this function sets the health bar initially full
    private void SetupHealthBar() {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    private void UpdateHealthBar() {
        // updates how much the bar is filled
        healthSlider.value = health;
        CheckDeath();
    }

    // this function destroys the enemy when the health goes below 0
    private void CheckDeath() {
        healthSlider.value = health;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
    
}
