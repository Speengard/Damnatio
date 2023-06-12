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
    [SerializeField] public PlayerStatsManager baseStats;
    [SerializeField] public PlayerStatsManager runStats;
    public int collectedSouls = 0;

    public static Player Instance { get; private set; }
    public GameManager gameManager;
    public GameObject portalArrow;

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
        //these two variables, are needed to keep track of the stats of the run compared to the base stats of the player
        baseStats = gameManager.playerStatsManager;
        runStats = new PlayerStatsManager(gameManager.playerStatsManager.playerCurrentStats);
    }

#region LootManager
    public void EnableLoot() {
        Vector3 targetPosition = transform.position;

        for (int i = 0; i < GameManager.Instance.lootObjects.Count; i++)
        {
            // make the objects move towards the player
            StartCoroutine(CollectObjects(GameManager.Instance.lootObjects[i]));

            // update the number of collected objects
            collectedSouls += 1;
        }
    }

    private IEnumerator CollectObjects(GameObject objectToMove) {
        Vector2 startPosition = objectToMove.transform.position;

        float distance = Vector3.Distance(startPosition, transform.position);
        float duration = distance / 10; // calculate the duration of the movement
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            objectToMove.transform.position = Vector3.Lerp(startPosition, transform.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = transform.position;

        // destroy the object
        Destroy(objectToMove);
    }
#endregion
}
