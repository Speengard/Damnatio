using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject lootPrefab;

    private void Start() {
        player = GameManager.Instance.player;
    }

    public void DropObjects(Enemy enemy) {
        float randomVariable = Random.Range(0, 1);
        if (randomVariable <= enemy.enemyStats.dropChance) {
            for(int i = 0; i < enemy.enemyStats.dropQuantity; i++) {
                GameObject lootObject = Instantiate(lootPrefab, enemy.transform.position, Quaternion.identity);
                GameManager.Instance.lootObjects.Add(lootObject);
            }
        }
    }

    public void CollectObjects() {
        Vector3 targetPosition = player.transform.position;

        for (int i = 0; i < GameManager.Instance.lootObjects.Count; i++)
        {
            Vector3 direction = (targetPosition - GameManager.Instance.lootObjects[i].transform.position).normalized;
            GameManager.Instance.lootObjects[i].transform.Translate(direction * 3 * Time.deltaTime);
        }
    }
}
