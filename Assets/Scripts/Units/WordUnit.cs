using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordUnit : MonoBehaviour
{
    public enum WordType 
    {
        DAMAGE,
        HEALING,
        PROTECTION
    }

    public WordType wordType = WordType.DAMAGE;
    public int damage;
    public int healing;
    public int protect;
    public string word;

}
