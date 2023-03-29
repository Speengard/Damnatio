using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private enum BattleState
    {
        START,
        PLAYERTURN,
        ENEMYTURN,
        WIN,
        LOSE
    }

    private BattleState _battleState = BattleState.START;

    //units and istantiated objects
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    private EnemyUnit enemyUnit;
    private PlayerUnit playerUnit;
    public List<GameObject> wordList;
    private List<GameObject> spawnableWords = new List<GameObject>();
    private List<GameObject> spawnedWords = new List<GameObject>();
    private List<GameObject> chain = new List<GameObject>();
    public GameObject selectedWord;
    public Canvas canvas;
    public GameObject swordPrefab;
    public GameObject shieldPrefab;
    public GameObject crossrefab;
    private List<GameObject> _spawnedSymbols = new List<GameObject>();
    
    //transform values to instantiate prefabs
    public Transform enemyBattleStation;
    public Transform playerBattleStation;
    public List<Transform> spawnPoints;
    public List<Transform> symbolSpawnPoint;

    //animation
    public GameObject scrollOpenAnimationPrefab;
    public GameObject scrollCloseAnimationPrefab;
    private GameObject _scrollOpenAnimationGO;
    private GameObject _scrollCloseAnimationGO;
    
    //UI variables
    public PlayerHUDManager playerHUDManager;
    public EnemyHUDManager enemyHUDManager;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        //instantiate the player and enemy prefabs
        _battleState = BattleState.START;
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);

        playerUnit = playerGO.GetComponent<PlayerUnit>();
        enemyUnit = enemyGO.GetComponent<EnemyUnit>();

        playerHUDManager.playerUnit = playerUnit;
        enemyHUDManager.unit = enemyUnit;
        
        UpdateHUD();

        //copying all of the words in the game to the player's inventory
        for (int i = 0; i < wordList.Count; i++)
        {
            playerUnit.wordList.Add(wordList[i]);
        }

        yield return new WaitForSeconds(1.5f);
        spawnableWords = new List<GameObject>(playerUnit.wordList);

        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        _battleState = BattleState.PLAYERTURN;
        foreach (var symbol in _spawnedSymbols)
        {
            Destroy(symbol);
        }
        
        _spawnedSymbols.Clear();
        _scrollOpenAnimationGO = Instantiate(scrollOpenAnimationPrefab, canvas.GetComponent<Transform>());
        yield return new WaitForSeconds(1.25f);
        Destroy(_scrollCloseAnimationGO);
        rerollAndDisplay();
    }
    private void rerollAndDisplay()
    {
        foreach (var word in spawnedWords)
        {
            Destroy(word);
        }

        spawnedWords.Clear();
        
        spawnableWords = Fisher_Yates_CardDeck_Shuffle(spawnableWords);
        int index = 0;
        
        foreach (var spawnPoint in spawnPoints)
        {
            GameObject tempGO = Instantiate(spawnableWords[index], spawnPoint);
            // StartCoroutine(fadeButton(tempGO.GetComponent<Button>(), true, 4f));
            tempGO.GetComponent<WordManager>().manager = this;
            tempGO.GetComponent<WordManager>().updateText();
            spawnedWords.Add(tempGO);
            index++;
        }
        
    }

    public void OnSelectWord()
    {
        if (chain.Count >= 2)
        {
            chain.Add(Instantiate(selectedWord));

            switch (chain.Last().GetComponent<WordUnit>().wordType)
            {
                case WordUnit.WordType.DAMAGE:
                    _spawnedSymbols.Add(Instantiate(swordPrefab,symbolSpawnPoint[chain.Count-1]));
                    break;
                case WordUnit.WordType.HEALING:
                    _spawnedSymbols.Add(Instantiate(crossrefab,symbolSpawnPoint[chain.Count-1]));
                    break;
                case WordUnit.WordType.PROTECTION:
                    _spawnedSymbols.Add(Instantiate(shieldPrefab,symbolSpawnPoint[chain.Count-1]));
                    break;
            }
            
            StartCoroutine(castSpell());
        }
        else
        {
            chain.Add(Instantiate(selectedWord));
            switch (chain.Last().GetComponent<WordUnit>().wordType)
            {
                case WordUnit.WordType.DAMAGE:
                    _spawnedSymbols.Add(Instantiate(swordPrefab,symbolSpawnPoint[chain.Count-1]));
                    break;
                case WordUnit.WordType.HEALING:
                    _spawnedSymbols.Add(Instantiate(crossrefab,symbolSpawnPoint[chain.Count-1]));
                    break;
                case WordUnit.WordType.PROTECTION:
                    _spawnedSymbols.Add(Instantiate(shieldPrefab,symbolSpawnPoint[chain.Count-1]));
                    break;
            }
            rerollAndDisplay();
        }
    }

    private void UpdateHUD()
    {
        playerHUDManager.updateHUD();
        enemyHUDManager.updateText();
    }

    public static List<GameObject> Fisher_Yates_CardDeck_Shuffle(List<GameObject> aList)
    {

        System.Random _random = new System.Random();

        GameObject myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }

    IEnumerator castSpell()
    {
        float _damageMultiplier = 1;
    float _protectMultiplier = 1;
    bool _healingMultiplier = false;

    switch (chain.First().GetComponent<WordUnit>().wordType)
    {
        case WordUnit.WordType.DAMAGE :
            _damageMultiplier = 1.5f;
            break;
        case WordUnit.WordType.PROTECTION:
            _protectMultiplier = 1.5f;
            break;
        case WordUnit.WordType.HEALING:
            _healingMultiplier = true;
        break;
    }

    foreach (var word in chain)
    {
        playerUnit.damage += (int)(word.GetComponent<WordUnit>().damage * _damageMultiplier);
        playerUnit.protect += (int)(word.GetComponent<WordUnit>().protect);
    }

    if (chain[0].GetComponent<WordUnit>().wordType == WordUnit.WordType.HEALING &&
        chain[1].GetComponent<WordUnit>().wordType == WordUnit.WordType.HEALING &&
        chain[2].GetComponent<WordUnit>().wordType == WordUnit.WordType.HEALING)
    {
        playerUnit.currentHP += playerUnit.healingValue;
    }
    if (chain[0].GetComponent<WordUnit>().wordType == WordUnit.WordType.PROTECTION &&
        chain[1].GetComponent<WordUnit>().wordType == WordUnit.WordType.PROTECTION &&
        chain[2].GetComponent<WordUnit>().wordType == WordUnit.WordType.PROTECTION)
    {
        playerUnit.protect = 300;
    }
    if (chain[0].GetComponent<WordUnit>().wordType == WordUnit.WordType.DAMAGE &&
        chain[1].GetComponent<WordUnit>().wordType == WordUnit.WordType.DAMAGE &&
        chain[2].GetComponent<WordUnit>().wordType == WordUnit.WordType.DAMAGE)
    {
        playerUnit.damage *= (int)(playerUnit.damage * _damageMultiplier);
    }
    
    
    Destroy(_scrollOpenAnimationGO);
    
    foreach (var word in spawnedWords)
    {
        Destroy(word);
    }
    foreach (var word in chain)
    {
        Destroy(word);
    }
    foreach (var symbol in _spawnedSymbols)
    {
        Destroy(symbol);
    }
    
    chain.Clear();
    _spawnedSymbols.Clear();
    _scrollCloseAnimationGO = Instantiate(scrollCloseAnimationPrefab, canvas.GetComponent<Transform>());
    yield return new WaitForSeconds(1.03f);
    Destroy(_scrollCloseAnimationGO);
    
    if (enemyUnit.takeDamage(playerUnit.damage))
    {
        //enemy dead
    }
    else
    {
        
        StartCoroutine(enemyTurn());    
    }
    playerUnit.resetValues();
    UpdateHUD();
    
    }

    IEnumerator enemyTurn()
    {
        _battleState = BattleState.ENEMYTURN;
        
        yield return new WaitForSeconds(2f);
        
        switch (enemyUnit.intent)
        {
            case EnemyUnit.ActionType.DAMAGE:
                playerUnit.takeDamage(enemyUnit.damage);
                break;
            case EnemyUnit.ActionType.HEAL:
                enemyUnit.currentHP += enemyUnit.heal;
                if (enemyUnit.currentHP > enemyUnit.maxHP)
                {
                    enemyUnit.currentHP = enemyUnit.maxHP;
                }
                break;
            case EnemyUnit.ActionType.DAMAGEPROTECT:
                playerUnit.takeDamage(enemyUnit.damage);
                break;
        }
        
        enemyUnit.resetValues();
        enemyUnit.getNewAction();
        UpdateHUD();
        StartCoroutine(PlayerTurn());
    }
    
    
    IEnumerator fadeButton(Button button, bool fadeIn, float duration)
    {

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0;
            b = 1;
        }
        else
        {
            a = 1;
            b = 0;
        }

        Image buttonImage = button.GetComponent<Image>();
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        //Enable both Button, Image and Text components
        if (!button.enabled)
            button.enabled = true;

        if (!buttonImage.enabled)
            buttonImage.enabled = true;

        if (!buttonText.enabled)
            buttonText.enabled = true;

        //For Button None or ColorTint mode
        Color buttonColor = buttonImage.color;
        Color textColor = buttonText.color;

        //For Button SpriteSwap mode
        ColorBlock colorBlock = button.colors;


        //Do the actual fading
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            //Debug.Log(alpha);

            if (button.transition == Selectable.Transition.None || button.transition == Selectable.Transition.ColorTint)
            {
                buttonImage.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, alpha);//Fade Traget Image
                buttonText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);//Fade Text
            }
            else if (button.transition == Selectable.Transition.SpriteSwap)
            {
                ////Fade All Transition Images
                colorBlock.normalColor = new Color(colorBlock.normalColor.r, colorBlock.normalColor.g, colorBlock.normalColor.b, alpha);
                colorBlock.pressedColor = new Color(colorBlock.pressedColor.r, colorBlock.pressedColor.g, colorBlock.pressedColor.b, alpha);
                colorBlock.highlightedColor = new Color(colorBlock.highlightedColor.r, colorBlock.highlightedColor.g, colorBlock.highlightedColor.b, alpha);
                colorBlock.disabledColor = new Color(colorBlock.disabledColor.r, colorBlock.disabledColor.g, colorBlock.disabledColor.b, alpha);

                button.colors = colorBlock; //Assign the colors back to the Button
                buttonImage.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, alpha);//Fade Traget Image
                buttonText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);//Fade Text
            }
            else
            {
                Debug.LogError("Button Transition Type not Supported");
            }

            yield return null;
        }

        if (!fadeIn)
        {
            //Disable both Button, Image and Text components
            buttonImage.enabled = false;
            buttonText.enabled = false;
            button.enabled = false;
        }
    }
}



