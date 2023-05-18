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
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private CardManager cardManager;
    public float roomWidth = 16;
    public float roomHeight = 10;
    private int sequence = 0;
    public bool isPlayerInstantiated;
    private GameObject player;
    private int numberOfEnemyType = 2;

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, numberOfEnemyType); // get a random prefab
            enemyPrefabs[index].GetComponent<Enemy>().spawnId = sequence;
            enemyPrefabs[index].GetComponent<Enemy>().target = player.transform;
            Instantiate(enemyPrefabs[index], RandomPointInScreen(), Quaternion.identity);
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
        enemiesToSpawn = CalculateEnemiesToSpawn(level);
        SpawnEnemies(); // spawn enemies only if you selected "start game"

        print("level:" + level);

        if ((level % 2) == 0) {
            cardManager.GenerateCards();
        }
    }

    private int CalculateEnemiesToSpawn(int level) {
        return (10 * level / 3);
    }

}
