using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private MovingEnemy movingEnemy;
    [SerializeField] private PlayerHealthController playerHealthController;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerHealthController.AddHealth(movingEnemy.damage);
            Debug.Log("eheheh toccato il player");
        }
    }
}
