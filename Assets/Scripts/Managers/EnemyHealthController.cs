using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : HealthController
{
    private Enemy enemy;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    private void Awake() {
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(spriteRenderer);
        }
        
        enemy = GetComponent<Enemy>();
    }
    //this script is used to manage the health of the enemy;
    //this script also updates the HUD when damage is taken.

    // this function destroys the enemy when the health goes below 0
    override public bool CheckDeath() {
        if (health <= 0)
        {

            GameManager.Instance.enemies.Remove(gameObject.GetComponent<Enemy>());

            // make the enemy drop something when it dies
            enemy.DropObjects();

            if (GameManager.Instance.enemies.Count == 0) {
                Debug.Log("last enemy");
                GameManager.Instance.player.EnableLoot();
            }

            Destroy(gameObject);
            
            return true;
        }else{
            return false;
        }

    }

    private void OnCollisionExit(Collision other)
    {
        CheckDeath();
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0){

            health = 0;
            CheckDeath();
        } 
        UpdateHealthBar(health);
        StartCoroutine(GiveRedTint());      
    }

    IEnumerator GiveRedTint(){

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = Color.red;
        }
        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = Color.white;
        }
    }

}
