//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private Sprite openPortalTexture;
    private bool flag = false;
    private void OnCollisionEnter2D(Collision2D other) {
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (GameManager.Instance.enemies.Count == 0)
        {

            GameManager.Instance.player.portalArrow.SetActive(false);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            LoadRandomScene();
        }
    }
    }

	private void LoadRandomScene() {
		int index = Random.Range(1, 4);
		SceneManager.LoadScene(index);
	}

    private void Update() {
        
        if(GameManager.Instance.enemies.Count == 0 && !flag) {
            GetComponentInParent<SpriteRenderer>().sprite = openPortalTexture;
            GameManager.Instance.player.portalArrow.SetActive(true);
            flag = true;
        }
    }
}
