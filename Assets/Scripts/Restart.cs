using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("contact happened");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
