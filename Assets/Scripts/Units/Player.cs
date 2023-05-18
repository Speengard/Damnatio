using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public PlayerAttackController attackController;
    public PlayerHealthController healthController;
    public PlayerMovementController movementController;
    
    [SerializeField] public PlayerStatsScriptableObject stats;
    [SerializeField] public PlayerStatsScriptableObject runStats;
    public GameManager gameManager;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        gameManager = GameManager.Instance;
    }

    void Start() {
        runStats = ScriptableObject.CreateInstance<PlayerStatsScriptableObject>();
    }

}
