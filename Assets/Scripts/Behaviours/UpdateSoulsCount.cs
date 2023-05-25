using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateSoulsCount : MonoBehaviour
{
    private TextMeshPro text;

    void Start() {
        text = GetComponent<TextMeshPro>();
        UpdateNumber();  
    }

    private void UpdateNumber() {
        int newValue = PlayerPrefs.GetInt("Animae", 0);
        text.text = newValue + " Animae";
    }
}
