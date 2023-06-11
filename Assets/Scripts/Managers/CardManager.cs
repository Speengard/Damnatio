using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject cardMenu;
    [SerializeField] private List<GameObject> cardButtons;
    [SerializeField] private Player player;
    private string path = "Cards/";
    private int numberOfCards = 2;
    private bool hasChosen = false;

    public IEnumerator GenerateCards(Action callback) {
        string spritePath;

        player = GameManager.Instance.player; // get player

        // list that helps keeping track of suits that have been already randomly selected
        List<CardSuits> availableSuits = new List<CardSuits>((CardSuits[])Enum.GetValues(typeof(CardSuits)));

        // pause game and make the cards appear
        Time.timeScale = 0;
        cardMenu.gameObject.SetActive(true);
        
        //selecting the cards
        for(int i = 0; i < numberOfCards; i++) {

            // get a random index within the available suits list
            int randomIndex = UnityEngine.Random.Range(0, availableSuits.Count);

            // get the selected suit at the random index
            CardSuits selectedSuit = availableSuits[randomIndex];

            // remove the selected suit from the available suits list
            availableSuits.RemoveAt(randomIndex);

            // get a value according to the current "run stats" of the player
            int selectedValue = GetStatsDifference(selectedSuit) + 1;

            SetText(cardButtons[i], selectedSuit, selectedValue);

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

        while(!hasChosen) {
            yield return 0;
        }
        hasChosen = false;
        callback.Invoke();
    }

    private void SetText(GameObject card, CardSuits suit, int value) {
        switch (suit) {
            case CardSuits.Cups: // power up for health points
                card.GetComponentInChildren<TextMeshProUGUI>().text = "Health +" + value;
                break;
            case CardSuits.Batons: // power up for ranged weapon
                card.GetComponentInChildren<TextMeshProUGUI>().text = "Ranged +" + value;
                break;
            case CardSuits.Swords: // power up for morning star
                card.GetComponentInChildren<TextMeshProUGUI>().text = "Morning Star +" + value;
                break;
            case CardSuits.Coins: // power up for collectibles
                card.GetComponentInChildren<TextMeshProUGUI>().text = "Luck +" + value;
                break;
            default:
                break;
        }
    }

    private void AssignPowerUp(CardSuits suit) {
        switch (suit) {
            case CardSuits.Cups: // power up for health points
                player.runStats.playerCurrentStats.health += 1;
                break;
            case CardSuits.Batons: // power up for ranged weapon
                player.runStats.playerCurrentStats.rangedDamage += 1;
                break;
            case CardSuits.Swords: // power up for morning star
                player.runStats.playerCurrentStats.morningStarDamage += 1;
                break;
            case CardSuits.Coins: // power up for collectibles
                player.runStats.playerCurrentStats.dropRate += 1;
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
                return player.runStats.playerCurrentStats.health - player.baseStats.playerCurrentStats.health;
            case CardSuits.Batons: // power up for ranged weapon
                return player.runStats.playerCurrentStats.rangedDamage - player.baseStats.playerCurrentStats.rangedDamage;
            case CardSuits.Swords: // power up for morning star
                return player.runStats.playerCurrentStats.morningStarDamage - player.baseStats.playerCurrentStats.morningStarDamage;
            case CardSuits.Coins: // power up for collectibles
                return player.runStats.playerCurrentStats.dropRate - player.baseStats.playerCurrentStats.dropRate;
            default:
                return 0;
        }
    }

    private void CloseCardMenu() {
        cardMenu.gameObject.SetActive(false);
        hasChosen = true;
        Time.timeScale = 1;
    }
}
