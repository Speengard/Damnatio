using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : HealthController
{

    public Material material;
    float fade = 1.0f;
    private Enemy enemy;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public List<GameObject> skeletons;

    private void Awake()
    {
        foreach(GameObject skeleton in skeletons){
            spriteRenderers.AddRange(skeleton.GetComponentsInChildren<SpriteRenderer>());
        }

        /* 
        foreach(SpriteRenderer s in spriteRenderers){
            s.material.SetFloat("_Fade",fade);
        }*/

        enemy = GetComponent<Enemy>();
    }
    //this script is used to manage the health of the enemy;
    //this script also updates the HUD when damage is taken.

    // this function destroys the enemy when the health goes below 0
    override public bool CheckDeath()
    {
        if (health <= 0)
        {
            UpdateHealthBar(health);

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Enemy>().stopMoving = true;

            StartCoroutine(FadeOut(fade, () => Destroy(gameObject)));

            // handle the death of the enemy
            GameManager.Instance.enemies.Remove(gameObject.GetComponent<Enemy>());
            
            GameManager.Instance.enemySlain += 1;

            Player.Instance.attackController.target = null;

            enemy.DropObjects(); // make the enemy drop something when it dies
            //Destroy(gameObject); // destroy enemy
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator FadeOut(float fade, Action callback = null)
    {
        while (fade > 0.0f)
        {
            fade -= 0.1f;

            foreach (SpriteRenderer s in spriteRenderers)
            {
                s.material.SetFloat("_Fade", fade);
            }

            yield return new WaitForSeconds(0.1f);
        }
        callback?.Invoke();
    }

    public override void TakeDamage(int damage)
    {

        health -= damage;

        if (health <= 0)
        {

            health = 0;
            CheckDeath();
            return;
        }

        UpdateHealthBar(health);
        StartCoroutine(GiveRedTint());
    }

    public IEnumerator GiveRedTint()
    {

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
