using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSuit {
    Swords,
    Cups,
    Coins,
    Batons
}

public class CardScriptableObject : ScriptableObject
{
    //[SerializeField] private GameObject cardPrefab;
    public int value;
    public CardSuit suit;
    public string cardImage;

}
