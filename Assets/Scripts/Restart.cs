//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("contact happened");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LoadRandomScene();
    }

	private void LoadRandomScene() {
		int index = Random.Range(0, 2);
		SceneManager.LoadScene(index);
	}
}
