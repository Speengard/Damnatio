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

public class LevelManager : MonoBehaviour
{
    //this class serves as a manager for the level (prefab spawning and entities spawning)
    [SerializeField] private int enemiesToSpawn;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private CardManager cardManager;
    public float roomWidth = 8;
    public float roomHeight = 6;
    private int sequence = 0;
    public bool isPlayerInstantiated;
    private GameObject player;
    

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            enemyPrefab.GetComponent<Enemy>().spawnId = sequence;
            enemyPrefab.GetComponent<Enemy>().target = player.transform;
            Instantiate(enemyPrefab, RandomPointInScreen(), Quaternion.identity);
            sequence++;
        }
    }

    private Vector2 RandomPointInScreen()
    {
        //Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        Vector2 randomPositionOnScreen = new Vector2(Random.Range(-roomWidth, roomWidth), Random.Range(-roomHeight, roomHeight));
        return randomPositionOnScreen;
    }

    public void InstantiatePlayer()
    {
        // the player is instantiated only once
        if (!GameManager.Instance.isPlayerInstantiated) {
            playerPrefab.GetComponent<PlayerMovementController>().Joystick = joystick;
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            PlayerPrefs.SetInt("isPlayerInstantiated", 1);
        } else {
            // reset player's position
            GameManager.Instance.player.transform.position = Vector3.zero;
        }
    }

    public void SetupScene(int level)
    {
        SpawnEnemies(); // spawn enemies only if you selected "start game"

        print("level:" + level);

        if ((level % 2) == 0) {
            cardManager.GenerateCards();
        }
    }

}
