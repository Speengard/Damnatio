using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] public Player player;
	[SerializeField] public PlayerStatsScriptableObject playerStats;
	[SerializeField] private GameObject gameOverScene;
	[SerializeField] private StartSceneManager startSceneManager;
	public static GameManager Instance { get; private set; }

	public float levelStartDelay = 2f;
	private LevelManager boardScript;
	public int level = 0;
	public List<Enemy> enemies;
	public List<GameObject> lootObjects;
	private bool enemiesMoving;
	public bool isPlayerInstantiated = false;
    public FollowPlayer followPlayer;

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
		lootObjects = new List<GameObject>();
		boardScript = GetComponent<LevelManager>();
	}

	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	private void Update() {
        if (level > 0) {
			if (followPlayer == null) followPlayer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
		}
	}
	
	public void GameOver()
	{
		Time.timeScale = 0;
		followPlayer.enabled = false;
		gameOverScene.gameObject.SetActive(true);
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
		lootObjects.Clear();
		boardScript.InstantiatePlayer(); // instantiate the player or reset the position in the scene

		// mark the player as instantiated
		isPlayerInstantiated = PlayerPrefs.GetInt("isPlayerInstantiated", 1) == 1;

		// get the index of the scene
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;

		// handle the level according to which kind of level the player is in
		if (sceneIndex != 0) {
			level += 1; // increment the level only if the player isn't in the "start scene"
			startSceneManager.enabled = false; // disable the manager of the "start scene"
			boardScript.SetupScene(level); // spawn enemies and enable the power-up system
		} else {
			// if we are the "start scene", enable its manager
			startSceneManager.enabled = true;
		}
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		InitGame();
	}
}
