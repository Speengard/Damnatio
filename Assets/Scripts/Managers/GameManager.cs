using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] public Player player;
	[SerializeField] public PlayerStatsScriptableObject playerStats;
	public static GameManager Instance { get; private set; }

	public float levelStartDelay = 2f;
	private LevelManager boardScript;
	private int level = 0;
	public List<Enemy> enemies;
	private bool enemiesMoving;
	public bool isPlayerInstantiated = false;

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

	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
		boardScript.InstantiatePlayer(); // instantiate the player or reset the position in the scene

		// mark the player as instantiated
		isPlayerInstantiated = PlayerPrefs.GetInt("isPlayerInstantiated", 1) == 1;

		// increment the level only if the player isn't in the "start scene"
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (sceneIndex != 0) {
			level += 1;
			boardScript.SetupScene(level); // spawn enemies and enable the power-up system
		}
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		InitGame();
	}
}
