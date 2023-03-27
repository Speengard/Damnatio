using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WordManager : MonoBehaviour
{
    public BattleManager manager;

    public void updateText()
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        WordUnit unit = GetComponent<WordUnit>();
        text.text = unit.word;
    }

    public void selectWord()
    {
        
        manager.selectedWord = gameObject;
        manager.OnSelectWord();
    }
}
