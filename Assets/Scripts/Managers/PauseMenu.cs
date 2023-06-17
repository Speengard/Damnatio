using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public void ResumeGame() {
        // disable camera shake
        if (GameManager.Instance.level > 0) GameManager.Instance.followPlayer.enabled = true;

        Time.timeScale = 1;
        CanvasManager.Instance.isShowingPause = false;
        gameObject.SetActive(false);
        
    }

    public void GetStartScene() {
        if(GameManager.Instance.level >0){
            
            GameManager.Instance.level = 0; // reset the level number
            GameManager.Instance.enemySlain = 0;
            SceneManager.LoadScene(0); // load the start scene
            // reset player health
            GameManager.Instance.player.healthController.healthSlider.
            gameObject.SetActive(false);

        }
        
        ResumeGame();
    }
}
