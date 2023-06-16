using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuitMenu : MonoBehaviour
{
    public TMP_Text animaeText;
    public TMP_Text enemiesText;

    private void OnEnable() {
        animaeText.text = "Animae: " + Player.Instance.collectedSouls;
        enemiesText.text = "Enemies slain " + GameManager.Instance.enemySlain;
    }


    public void ResumeGame()
    {
        // disable camera shake
        if (GameManager.Instance.level > 0) GameManager.Instance.followPlayer.enabled = true;

        Time.timeScale = 1;
    }

    public void OnClick()
    {
        GameManager.Instance.level = 0; // reset the level number
        SceneManager.LoadScene(0); // load the start scene
        GameManager.Instance.enemySlain = 0;
        // reset player health
        GameManager.Instance.player.healthController.healthSlider.gameObject.SetActive(false);

        ResumeGame();
    }
}
