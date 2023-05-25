using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : HealthController
{

    public List<Material> materials;
    float fade = 1.0f;

    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    private void Awake() {
        
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(spriteRenderer);
            materials.Add(GetComponentInChildren<SpriteRenderer>().material);
        }

        foreach (Material material in materials)
        {
            material.SetFloat("_Fade", fade);
        }
    }
    //this script is used to manage the health of the enemy;
    //this script also updates the HUD when damage is taken.

    // this function destroys the enemy when the health goes below 0
    override public bool CheckDeath() {
        if (health <= 0)
        {
            UpdateHealthBar(health);
            
            StartCoroutine(FadeOut(fade, () => {
                print("destroying enemy");
                GameManager.Instance.enemies.Remove(gameObject.GetComponent<Enemy>());
                Destroy(gameObject);
            }));
            
            return true;
        }else{
            return false;
        }

    }
    IEnumerator FadeOut(float fade, Action onComplete = null){

        while (fade > 0.0f)
        {
            print("fading out");
            fade -= 0.1f;
            foreach (Material material in materials)
            {
                material.SetFloat("_Fade", fade);
            }
            yield return new WaitForSeconds(0.2f);
        }

        onComplete?.Invoke();
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0){

            health = 0;
            CheckDeath();
            return;
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
