using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class RandomCardGenerator : MonoBehaviour
{
    [SerializeField] List<string> cardNames;
    [SerializeField] GameObject cardMenu;

    void Start()
    {
        for (int i = 0; i < 4; i++) {
            cardNames.Add("Carta n. " + (i+1));
        }     
    }

    public void GenerateCards() {
        Time.timeScale = 0;
        cardMenu.gameObject.SetActive(true);
        
        for(int i = 0; i < 2; i++) {
            int index = Random.Range(0,4);
            Debug.Log(cardNames[index]);
        }
    }
}
