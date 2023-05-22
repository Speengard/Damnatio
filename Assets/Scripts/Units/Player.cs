using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    ///this class is used as an interface for any other class needing to access the components of the player. it is singleton
    /// //Component references
    public PlayerAttackController attackController;
    public PlayerHealthController healthController;
    public PlayerAnimationController animationController;
    public PlayerMovementController movementController;
    public Rigidbody2D rb;
    [SerializeField] public PlayerStatsScriptableObject stats;
    [SerializeField] public PlayerStatsScriptableObject runStats;

    public static Player Instance { get; private set; }
    public GameManager gameManager;

    void Awake() {

        if(Player.Instance == null){
            Instance = this;
        }else if(Player.Instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        gameManager = GameManager.Instance;
    }

    void Start() {
        runStats = ScriptableObject.CreateInstance<PlayerStatsScriptableObject>();
    }
}
