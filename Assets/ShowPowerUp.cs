using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPowerUp : MonoBehaviour
{
    private CircleCollider2D cd;
    public GameObject toShow;
    private void Awake() {
        cd = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            Time.timeScale = 0;
            toShow.SetActive(true);
        }
    }
}
