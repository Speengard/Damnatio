using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Enemy : MovingEnemy
{
    public int damage; // damage that the enemy does on the player
    [SerializeField] private EnemyHealthController healthController; 
    public int ID;


    private void Start()
    {
        GameManager.Instance.AddEnemyToList(this);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        healthController.SetupHealthBar(); 
    }

}
