using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject cardMenu;
    [SerializeField] private List<GameObject> cardButtons;
    [SerializeField] private Player player;
    private string path = "Cards/";
    private int numberOfCards = 2;

    public void GenerateCards() {
        string spritePath;

        player = GameManager.Instance.player; // get player

        // list that helps keeping track of suits that have been already randomly selected
        List<CardSuits> availableSuits = new List<CardSuits>((CardSuits[])Enum.GetValues(typeof(CardSuits)));

        // pause game and make the cards appear
        Time.timeScale = 0;
        cardMenu.gameObject.SetActive(true);
        
        for(int i = 0; i < numberOfCards; i++) {
            // get a random index within the available suits list
            int randomIndex = UnityEngine.Random.Range(0, availableSuits.Count);

            // get the selected suit at the random index
            CardSuits selectedSuit = availableSuits[randomIndex];

            // remove the selected suit from the available suits list
            availableSuits.RemoveAt(randomIndex);

            // get a value according to the current "run stats" of the player
            int selectedValue = GetStatsDifference(selectedSuit);

            // JUST FOR TEST: since we don't have other cards yet, force the value as 1
            selectedValue = 1;

            // build sprite path (Resources/Cards/valueOfSuit)
            spritePath = path + selectedValue + "Of" + selectedSuit;

            // display the right sprite for the generated card
            cardButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath);   

            // remove previous listeners so that only the targeted stat is updated
            cardButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            cardButtons[i].GetComponent<Button>().onClick.AddListener(() => AssignPowerUp(selectedSuit));
        }
    }

    private void AssignPowerUp(CardSuits suit) {
        switch (suit) {
            case CardSuits.Cups: // power up for health points
                player.runStats.health += 1;
                break;
            case CardSuits.Batons: // power up for ranged weapon
                player.runStats.rangedDamage += 1;
                break;
            case CardSuits.Swords: // power up for morning star
                player.runStats.morningStarDamage += 1;
                break;
            case CardSuits.Coins: // power up for collectibles
                player.runStats.dropChance += 1;
                break;
            default:
                break;
        }

        CloseCardMenu();
    }

    // this function calculates the difference between the "run stats" and the "stats"
    // in order to get the card value to display
    private int GetStatsDifference(CardSuits suit) {
        switch (suit) {
            case CardSuits.Cups: // power up for health points
                return player.runStats.health - player.stats.health;
            case CardSuits.Batons: // power up for ranged weapon
                return player.runStats.rangedDamage - player.stats.rangedDamage;
            case CardSuits.Swords: // power up for morning star
                return player.runStats.morningStarDamage - player.stats.morningStarDamage;
            case CardSuits.Coins: // power up for collectibles
                return player.runStats.dropChance - player.stats.dropChance;
            default:
                return 0;
        }
    }

    private void CloseCardMenu() {
        cardMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
