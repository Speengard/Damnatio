using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public LayerMask collisionLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d;
    public Transform target;
    [SerializeField] public EnemyHealthController healthController;
    [SerializeField] private EnemyStatsScriptableObject enemyStats;

    public int damage; // damage that the enemy does on the player
    public int spawnId;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        healthController.SetupHealthBar(enemyStats.health);
        damage = enemyStats.damage;
    }
}
