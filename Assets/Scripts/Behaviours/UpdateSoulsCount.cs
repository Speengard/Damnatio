using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateSoulsCount : MonoBehaviour
{
    private TextMeshPro text;

    void Awake() {
        text = GetComponent<TextMeshPro>();
        UpdateNumber();  
    }

    private void OnEnable() {
        UpdateNumber();
    }

    private void UpdateNumber() {
        //int newValue = PlayerPrefs.GetInt("Animae", 0);
        int newValue = GameManager.Instance.playerStatsManager.playerCurrentStats.collectedSouls;
        text.text = newValue + " Animae";
    }
}
