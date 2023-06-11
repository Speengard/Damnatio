using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] private GameObject gameOverScene;
    [SerializeField] private StartSceneManager startSceneManager;
    [SerializeField] private GameObject onboardingScreen;
    public static GameManager Instance { get; private set; }
    public float levelStartDelay = 2f;
    private LevelManager boardScript;
    public int level = 0;
    public List<Enemy> enemies;
    public List<GameObject> lootObjects;
    public bool isPlayerInstantiated = false;
    public FollowPlayer followPlayer;
    public GameObject laserSlider;

    [SerializeField] private GameDataManager gameDataManager;
    public PlayerStatsManager playerStatsManager;

    void Awake()
    {

        if (Instance == null) Instance = this;

        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //checks if this is the first time the game is being played and if so, initializes the player's stats to a new file
        if (gameDataManager.readPlayerFile() == null)
        {

            playerStatsManager = new PlayerStatsManager();
            
            gameDataManager.writePlayerData(playerStatsManager);

        }else{
            //I create a copy of the player's stats so that I can modify them without modifying the original
            playerStatsManager = gameDataManager.readPlayerFile();

		}

        print("reading back:" + gameDataManager.readPlayerFile());

        // init the onboarding by enabling the canvas object
        // if (!PlayerPrefs.HasKey("isFirstLaunch")) {
            // onboardingScreen.SetActive(true);
        // }

        enemies = new List<Enemy>();
        lootObjects = new List<GameObject>();
        boardScript = GetComponent<LevelManager>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (level > 0)
        {
            if (followPlayer == null) followPlayer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
        }
    }

	public void saveStats(){
		gameDataManager.writePlayerData(playerStatsManager);
		playerStatsManager = gameDataManager.readPlayerFile();
	}

    public void GameOver()
    {
        Time.timeScale = 0;
        
        followPlayer.enabled = false;
        gameOverScene.gameObject.SetActive(true);

        // update the value of Animae
        playerStatsManager.playerCurrentStats.collectedSouls += Player.Instance.collectedSouls;

        gameDataManager.writePlayerData(playerStatsManager);
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
        if (sceneIndex != 0)
        {
            level += 1; // increment the level only if the player isn't in the "start scene"
            startSceneManager.enabled = false; // disable the manager of the "start scene"
            boardScript.SetupScene(level); // spawn enemies and enable the power-up system
            

        }
        else
        {
            // if we are the "start scene", enable its manager
            startSceneManager.enabled = true;
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        InitGame();
    }

    

}
