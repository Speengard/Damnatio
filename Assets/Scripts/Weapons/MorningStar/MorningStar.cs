using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MorningStar : MonoBehaviour
{
    [SerializeField] private int morningStarDamage;
    [SerializeField] private GameObject finalLink;
    public float angularVelocity;
    public float damage;
    public bool hasHit = false;

    private void Update() {
        angularVelocity = GetComponent<Rigidbody2D>().angularVelocity;
        damage = morningStarDamage;
    }
    private void Start() {
        morningStarDamage = Player.Instance.runStats.playerCurrentStats.morningStarDamage;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyHealthController enemyHealthController = other.gameObject.GetComponent<EnemyHealthController>();

        if (other.gameObject.CompareTag("Enemy")) {
            hasHit = true;
        }

        if (other.gameObject.CompareTag("Enemy") && enemyHealthController != null)
        {
            if(other.rigidbody != null){

            other.rigidbody.AddForce(transform.up * 0.3f);

            }

            switch(Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity)){
                case <30:

                break;

                case <50:
                
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(morningStarDamage);
                    break;
                case <100:
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(((int)(morningStarDamage * 1.1)));
                    break;
                case <170:
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(((int)(morningStarDamage * 1.3)));
                    break;
                case <200:
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(((int)(morningStarDamage * 1.4)));
                    break;
                default:
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(((int)(morningStarDamage * 1.5)));
                    break;
            }
        }

        StartCoroutine(ResetHitFlag());
    }

    private IEnumerator ResetHitFlag()
    {
        yield return new WaitForSeconds(0.5f);
        hasHit = false;
    }
    
}
