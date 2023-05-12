using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Player player;
	[SerializeField] public PlayerStatsScriptableObject playerStats;
	public static GameManager Instance { get; private set; }

	public float levelStartDelay = 2f;
	private LevelManager boardScript;
	private int level = 0;
	public List<Enemy> enemies;
	private bool enemiesMoving;

	void Awake()
	{
		if (Instance == null) Instance = this;
		
		if (Instance != this) 
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		enemies = new List<Enemy>();
		boardScript = GetComponent<LevelManager>();
		boardScript.isPlayerInstantiated = PlayerPrefs.GetInt("isPlayerInstantiated", 0) == 1;
	}
	
	public void GameOver()
	{
		enabled = false;
	}

	public void AddEnemyToList(Enemy enemy)
	{
		enemies.Add(enemy);
	}

	void OnEnable()
	{	
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
	
	void InitGame()
	{
		enemies.Clear();
		boardScript.SetupScene(level);
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		level += 1;
		InitGame();
	}
}
