using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScene;

    public void PauseGame() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
        gameOverScene.SetActive(false);

        // disable camera shake
        if (GameManager.Instance.level > 0) GameManager.Instance.followPlayer.enabled = true;

        Time.timeScale = 1;
    }

    public void GetStartScene() {
        GameManager.Instance.level = 0; // reset the level number
        SceneManager.LoadScene(0); // load the start scene

        // reset player health
        GameManager.Instance.player.healthController.healthSlider.gameObject.SetActive(false);
        
        ResumeGame();
    }
}
