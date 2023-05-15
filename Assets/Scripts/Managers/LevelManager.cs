using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class LevelManager : MonoBehaviour
{
    //this class serves as a manager for the level (prefab spawning and entities spawning)
    [SerializeField] private int enemiesToSpawn;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject borders;
    [SerializeField] private FloatingJoystick joystick;
    public float roomWidth = 8;
    public float roomHeight = 6;
    private int sequence = 0;
    public bool isPlayerInstantiated;

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, RandomPointInScreen(), Quaternion.identity).GetComponent<Enemy>().spawnId = sequence;
            sequence++;
        }
    }

    private Vector2 RandomPointInScreen()
    {
        //Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        Vector2 randomPositionOnScreen = new Vector2(Random.Range(-roomWidth, roomWidth), Random.Range(-roomHeight, roomHeight));
        return randomPositionOnScreen;
    }

    public void SetupScene(int level)
    {
        // the player is instantiated only once
        if (!GameManager.Instance.isPlayerInstantiated) {
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerMovementController>().Joystick = joystick;
            PlayerPrefs.SetInt("isPlayerInstantiated", 1);
        } else {
            // reset player's position
            GameManager.Instance.player.transform.position = Vector3.zero;
        }

        if (level > 0)  SpawnEnemies(); // spawn enemies only if you selected "start game"

        print("level:" + level);
    }

}
