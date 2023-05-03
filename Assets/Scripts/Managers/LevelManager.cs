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

    private Player player;
    
    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, RandomPointInScreen(), Quaternion.identity);
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
            Instantiate(groundPrefab, Vector3.zero, Quaternion.identity);
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerMovementController>().Joystick = joystick;
            //Instantiate(borders, Vector3.zero, Quaternion.identity);
            SpawnEnemies();
            print("level:" + level);
        }
    
    }
