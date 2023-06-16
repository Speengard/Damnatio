using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            GameManager.Instance.lootObjects.Remove(gameObject);
            Player.Instance.collectedSouls += 1;
            Destroy(gameObject);
        }
    }
}
