using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            GameManager.Instance.lootObjects.Remove(gameObject);
            Player.Instance.collectedSouls += 1;
            Destroy(gameObject);
        }
    }

    private void Update() {
        if(GameManager.Instance.enemies.Count == 0){

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y), 8f * Time.deltaTime);

        }
    }
}
