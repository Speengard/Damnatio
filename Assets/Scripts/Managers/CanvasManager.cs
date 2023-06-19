using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject countDown;
    [SerializeField] private GameObject powerUp;

    public bool isShowingPause = false;
    public bool isShowingGameOver = false;
    public bool isShowingCountDown = false;
    public bool isShowingPowerUp = false;

    public bool flag = false;

    public static CanvasManager Instance { get; private set; } 
    private void Awake()
    {
        if (Instance == null) Instance = this;
		
        if (Instance != this) 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    
    public void showPauseMenu(){
        if(gameOverMenu.activeSelf == false){
            isShowingPause = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }   

    public void showGameOver(){
        if (pauseMenu.activeSelf == false)
        {
            isShowingGameOver = true;
            gameOverMenu.SetActive(true);
        }
    }

    public IEnumerator showCountDown(Action callback)
    {
        isShowingCountDown = true;
        countDown.SetActive(true);
        countDown.GetComponentInChildren<TMP_Text>().text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countDown.GetComponentInChildren<TMP_Text>().text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countDown.GetComponentInChildren<TMP_Text>().text = "1";
        yield return new WaitForSecondsRealtime(1f);
        countDown.GetComponentInChildren<TMP_Text>().text = "GO!";
        yield return new WaitForSecondsRealtime(1f);
        countDown.SetActive(false);
        isShowingCountDown = false;
        callback.Invoke();
    }

    public void showPowerUp(){
        isShowingPowerUp = true;
        powerUp.SetActive(true);
    }

    


}
