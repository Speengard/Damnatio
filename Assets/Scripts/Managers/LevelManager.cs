using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using TMPro;

public class LevelManager : MonoBehaviour
{
    //this class serves as a manager for the level (prefab spawning and entities spawning)
    [SerializeField] private int enemiesToSpawn;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private CardManager cardManager;
    public float roomWidth = 7;
    public float roomHeight = 3;
    private int sequence = 0;
    public bool isPlayerInstantiated;
    private GameObject player;
    private int numberOfEnemyType = 2;
    public GameObject countDown;
    public TMP_Text countDownText;

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, numberOfEnemyType); // get a random prefab
            enemyPrefabs[index].GetComponent<Enemy>().spawnId = sequence;
            enemyPrefabs[index].GetComponent<Enemy>().target = player.transform;
            GameObject enemyGameObject = Instantiate(enemyPrefabs[index], RandomPointInScreen(), Quaternion.identity);
            AddEnemyToList(enemyGameObject.GetComponent<Enemy>());
            sequence++;
        }
    }

    private Vector2 RandomPointInScreen()
    {
        float halfDiamondSize = Mathf.Min(roomWidth / 2f, roomHeight / 2f);
        float randomX = Random.Range(-1f, 1f) * halfDiamondSize;
        float randomY = Random.Range(-1f, 1f) * halfDiamondSize;

        //Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        Vector2 randomPositionOnScreen = new Vector2(randomX, randomY);
        return randomPositionOnScreen;
    }

    public void InstantiatePlayer()
    {
        // the player is instantiated only once
        if (!GameManager.Instance.isPlayerInstantiated)
        {
            playerPrefab.GetComponent<PlayerMovementController>().Joystick = joystick;
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            PlayerPrefs.SetInt("isPlayerInstantiated", 1);
        }
        else
        {
            // reset player's position
            GameManager.Instance.player.transform.position = Vector3.zero;
        }
    }

    public void SetupScene(int level)
    {
        enemiesToSpawn = CalculateEnemiesToSpawn(level);
        SpawnEnemies(); // spawn enemies only if you selected "start game"

        StartCoroutine(SpawnCards(level));

        

    }

    private int CalculateEnemiesToSpawn(int level)
    {
        return (10 * level / 3);
    }

    public void AddEnemyToList(Enemy enemy)
    {
        GameManager.Instance.enemies.Add(enemy);
    }

    IEnumerator SpawnCards(int level)
    {
        if ((level % 2) == 0)
        {
            StartCoroutine(cardManager.GenerateCards(() =>
            {
                Time.timeScale = 0;
                StartCoroutine(WaitBeforeStart(() =>
                {
                    Player.Instance.enabled = false;
                    Time.timeScale = 1;
                }));
            }));
            yield return 0;
        }else{
                Time.timeScale = 0;
                StartCoroutine(WaitBeforeStart(() =>
                {
                    Player.Instance.enabled = false;
                    Time.timeScale = 1;
                }));
        }

    }

    IEnumerator WaitBeforeStart(Action callback)
    {
        countDown.SetActive(true);
        countDownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countDownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countDownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        countDownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);
        countDown.SetActive(false);
        callback.Invoke();
    }

}
