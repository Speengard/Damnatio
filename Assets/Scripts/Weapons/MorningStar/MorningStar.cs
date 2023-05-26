using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MorningStar : MonoBehaviour
{
    [SerializeField] private int morningStarDamage;
    [SerializeField] private GameObject finalLink;

    private void Start() {
        morningStarDamage = gameObject.GetComponentInParent<Player>().runStats.morningStarDamage;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            if(other.rigidbody != null){

            other.rigidbody.AddForce(transform.up * 2);
            }

            switch(Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity)){
                case <50:
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(morningStarDamage);
                    break;
                case <100:
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(((int)(morningStarDamage * 1.5)));
                    break;
                case <170:
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(((int)(morningStarDamage * 1.8)));
                    break;
                case <200:
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(((int)(morningStarDamage * 2)));
                    break;
                default:
                    other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(((int)(morningStarDamage * 2.5)));
                    break;
            }
        }
    }
    
}
