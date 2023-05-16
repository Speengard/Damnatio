using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider healthSlider;
    private float roomWidth = Screen.width;
    private float roomHeight = Screen.height;

    // Start is called before the first frame update
    void Start()
    {
        // disable scripts that handle enemies and define the room size
        GameManager.Instance.GetComponent<LevelManager>().enabled = false;
        player = GameManager.Instance.player.GetComponent<Player>();

        // assign the health bar to the player
        player.healthController.healthSlider = healthSlider;
    }

    // when the player select "Start game" enable the script and the health bar
    private void OnDestroy() {
        GameManager.Instance.GetComponent<LevelManager>().enabled = true;

        SetupPlayerHealthBar();
    }

    // setup the health bar and make it visible
    private void SetupPlayerHealthBar() {
        player.healthController.SetupHealthBar(player.stats.health);
        player.healthController.EnableHealthBar();
    }
}
