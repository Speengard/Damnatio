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
    [SerializeField] private int enemiesToSpawn;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject playerPrefab;

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
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        return randomPositionOnScreen;
    }

    public void SetupScene(int level)
    {
        Instantiate(groundPrefab, Vector3.zero, Quaternion.identity);
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        SpawnEnemies();
        print("level:" + level);
    }

    private void Start()
    {
        SpawnEnemies();
    }
}
