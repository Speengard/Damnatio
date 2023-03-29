using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.PlayerLoop;

public class WordManager : MonoBehaviour
{
    public BattleManager manager;
    private TMP_Text textBox;
    private WordUnit unit;
    private int index = 0;

    public void updateText()
    {
        textBox = GetComponentInChildren<TMP_Text>();
        unit = GetComponent<WordUnit>();
        
        textBox.text = "";

        reproduceText();
    }

    private void reproduceText()
    {
        if (textBox.text.Length < unit.word.Length)
        {
            char letter = unit.word[index];
            textBox.text += letter;

            index ++;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.17f);
        reproduceText();
    }

    public void selectWord()
    {
        
        manager.selectedWord = gameObject;
        manager.OnSelectWord();
    }
}
