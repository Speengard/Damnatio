using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("contact happened");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
