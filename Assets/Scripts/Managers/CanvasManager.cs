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
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject switchButton;
    [SerializeField] private GameObject laserSlider;
    [SerializeField] private GameObject animaeText;

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

    private void Update() {
        if(GetComponent<Canvas>().worldCamera == null){
            GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
    
    public void showPauseMenu(){
        if(gameOverMenu.activeSelf == false){
            hideSwitch();
            isShowingPause = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }   

    public void showGameOver(){
        if (pauseMenu.activeSelf == false)
        {
            hideSwitch();
            isShowingGameOver = true;
            gameOverMenu.SetActive(true);
        }
    }

    public IEnumerator showCountDown(Action callback)
    {
        hideSwitch();
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
        showSwitch();
        callback.Invoke();
    }

    public void showPowerUp(){
        isShowingPowerUp = true;
        hideSwitch();
        pauseButton.SetActive(false);
        powerUp.SetActive(true);
    }

    public void showSwitch(){
        switchButton.SetActive(true);
    }

    public void hideSwitch(){

        if(switchButton.GetComponent<SwitchWeapon>().isChanging){
            switchButton.GetComponent<SwitchWeapon>().StopAllCoroutines();
            switchButton.GetComponent<SwitchWeapon>().slider.value = 2.0f;
            switchButton.GetComponent<SwitchWeapon>().isChanging = false;
        }
        
        switchButton.SetActive(false);
    }

    public void hideScoreText(){
        animaeText.SetActive(false);
    }

    public void showScoreText(){
        animaeText.SetActive(true);
    }

    public void hideLaserSlider(){
        laserSlider.SetActive(false);
    }

    public void showLaserSlider(){
        laserSlider.SetActive(true);
    }

}
