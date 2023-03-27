using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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

    //transform values to instantiate prefabs
    public Transform enemyBattleStation;
    public Transform playerBattleStation;
    public List<Transform> spawnPoints;
    
    //animation
    public GameObject scrollOpenAnimationPrefab;
    public GameObject scrollCloseAnimationPrefab;
    private GameObject _scrollOpenAnimationGO;
    private GameObject _scrollCloseAnimationGO;

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
            StartCoroutine(castSpell());
        }
        else
        {
            chain.Add(Instantiate(selectedWord));
            rerollAndDisplay();
        }
    }

    private void UpdateHUD()
    {
        
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
        float _damageMultiplier = 0;
    float _protectMultiplier = 0;
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
        playerUnit.damage *= (int)(playerUnit.damage * 1.5);
    }
    
    
    enemyUnit.takeDamage(playerUnit.damage);
    playerUnit.resetValues();
    Destroy(_scrollOpenAnimationGO);
    
    foreach (var word in spawnedWords)
    {
        Destroy(word);
    }
    foreach (var word in chain)
    {
        Destroy(word);
    }
    chain.Clear();
    _scrollCloseAnimationGO = Instantiate(scrollCloseAnimationPrefab, canvas.GetComponent<Transform>());
    yield return new WaitForSeconds(1.2f);
    Destroy(_scrollCloseAnimationGO);
    StartCoroutine(enemyTurn());
    
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
                break;
            case EnemyUnit.ActionType.DAMAGEPROTECT:
                playerUnit.takeDamage(enemyUnit.damage);
                enemyUnit.currentHP += enemyUnit.heal;
                break;
        }

        enemyUnit.resetValues();
        enemyUnit.getNewAction();
        StartCoroutine(PlayerTurn());
    }
    
}



