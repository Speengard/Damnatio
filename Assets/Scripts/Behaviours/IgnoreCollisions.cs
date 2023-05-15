using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Mace") || other.gameObject.CompareTag("MorningStar") || other.gameObject.CompareTag("MorningStarLink") || other.gameObject.CompareTag("Enemy")) {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other.collider);
        }
    }
}
