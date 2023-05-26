using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public LayerMask collisionLayer;
    private BoxCollider2D boxCollider;
    public Rigidbody2D rb2d;
    public Transform target;
    [SerializeField] private EnemyHealthController healthController;
    [SerializeField] public EnemyStatsScriptableObject enemyStats;

    [SerializeField] public GameObject lootPrefab;

    public bool stopMoving = false;

    public int damage; // damage that the enemy does on the player
    public int spawnId;

    public void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        
        healthController.SetupHealthBar(enemyStats.health);
        damage = enemyStats.damage;
    }

    public void TakeDamage(int damage)
    {
        healthController.TakeDamage(damage);
    }

    public void DropObjects() {
        // calculate the probability
        float randomVariable = Random.Range(0.0f, 1.0f);
        if (randomVariable <= (enemyStats.dropChance / 100)) {
            // generate as many loot objects as the enemy can drop 
            for (int i = 0; i < enemyStats.dropQuantity; i++)
            {
                GameObject lootObject = Instantiate(lootPrefab, transform.position, Quaternion.identity);
                GameManager.Instance.lootObjects.Add(lootObject);
            }
        }
    }

}
