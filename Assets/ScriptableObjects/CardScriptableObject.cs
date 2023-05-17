using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSuits {
    Swords = 0,
    Cups = 1,
    Coins = 2,
    Batons = 3
}

public class CardScriptableObject : ScriptableObject
{
    //[SerializeField] private GameObject cardPrefab;
    public int value;
    public CardSuits suit;
    public string cardImage;

}
