using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider healthSlider;

    // setup the health bar and make it visible
    private void SetupPlayerHealthBar() {
        player.healthController.SetupHealthBar(player.baseStats.playerCurrentStats.health);
        player.healthController.EnableHealthBar();
    }

    private void OnEnable() {
        // disable scripts that handle enemies and define the room size
        GameManager.Instance.GetComponent<LevelManager>().enabled = false;
    }

    // the GameManager disables this object when an actual playable level is loaded
    // so at this point we set the references for the player and setup the health bar
    private void OnDisable() {
        player = GameManager.Instance.player;

        // assign the health bar to the player
        player.healthController.healthSlider = healthSlider;

        GameManager.Instance.GetComponent<LevelManager>().enabled = true;

        SetupPlayerHealthBar();
    }
}
