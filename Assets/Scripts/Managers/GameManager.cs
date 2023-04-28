using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
	public static GameManager Instance { get; private set; }

	public float levelStartDelay = 2f;

	private LevelManager boardScript;
	private bool doingSetup;

	private int level = 0;
	private List<Enemy> enemies;
	private bool enemiesMoving;
	[SerializeField] private int playerHealth = 100; // initial health; TODO: test the right value

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
		doingSetup = true;
		
		enemies.Clear();
		boardScript.SetupScene(level);
	}
	

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		level += 1;
		InitGame();
	}

	public void AddHealth(int value) {
		playerHealth += value;

		// check if the player is dead
		if (playerHealth <= 0) {
			Debug.Log("Game Over!"); // TODO: Game over scene
			playerHealth = 0;
		}

		Debug.Log("Health Bar = " + playerHealth);
	}
}
