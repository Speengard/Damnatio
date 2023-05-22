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
    public int collectedSouls = 0;

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

    public void EnableLoot() {
        Vector3 targetPosition = transform.position;

        for (int i = 0; i < GameManager.Instance.lootObjects.Count; i++)
        {
            Debug.Log("Entro qua n. " + i);
            
            StartCoroutine(CollectObjects(GameManager.Instance.lootObjects[i]));

            collectedSouls += 1;
        }
    }

    private IEnumerator CollectObjects(GameObject objectToMove) {
        Vector2 startPosition = objectToMove.transform.position;
        Vector2 targetPosition = transform.position;

        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / 10;
        float elapsedTime = 0f;

        Debug.Log("corout");

        while (elapsedTime < duration) {
            objectToMove.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = targetPosition;

        // L'oggetto ha raggiunto la posizione target, puoi eseguire le azioni desiderate qui
        Debug.Log("ok finito.");
    }
}
