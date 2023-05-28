using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public List<SpriteRenderer> healthCapsules = new List<SpriteRenderer>();
    public List<SpriteRenderer> dropCapsules = new List<SpriteRenderer>();
    public List<SpriteRenderer> damageCapsules = new List<SpriteRenderer>();

    public TMP_Text healthText;
    public TMP_Text dropText;
    public TMP_Text damageText;

    public TMP_Text healthCostText;
    public TMP_Text dropCostText;
    public TMP_Text damageCostText;

    public Button healthButton;
    public Button dropButton;
    public Button damageButton;
    PlayerStatsManager playerStatsManager;
    public Sprite fullCapsule;

    public int[] healthCosts = { 150, 450, 750, 1050, 1500 };
    public int[] damageCosts = { 150, 450, 750, 1050, 1500 };
    public int[] dropcosts = { 150, 450, 750, 1500, 4000 };

    public GameObject pauseButton;
    public GameObject floatingJoystick;

    private void OnEnable() {
        playerStatsManager = GameManager.Instance.playerStatsManager;
        Player.Instance.movementController.enabled = false;
        pauseButton.SetActive(false);
        floatingJoystick.SetActive(false);
        initScreen();
    }

    private void OnDisable() {
        Player.Instance.movementController.enabled = true;
        pauseButton.SetActive(false);
        floatingJoystick.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.saveStats();
    }


    private void initScreen(){
        healthText.text = "Health: - level " + playerStatsManager.playerCurrentStats.healthLevel;
        dropText.text = "Drop: - level " + playerStatsManager.playerCurrentStats.dropLevel;
        damageText.text = "Damage: - level " + playerStatsManager.playerCurrentStats.damageLevel;

        for (int i = 0; i < playerStatsManager.playerCurrentStats.healthLevel-1; i++)
        {
            healthCapsules[i].sprite = fullCapsule;
        }
        for (int i = 0; i < playerStatsManager.playerCurrentStats.dropLevel-1; i++)
        {
            dropCapsules[i].sprite = fullCapsule;
        }
        for (int i = 0; i < playerStatsManager.playerCurrentStats.damageLevel-1; i++)
        {
            damageCapsules[i].sprite = fullCapsule;
        }

        healthCostText.text = healthCosts[playerStatsManager.playerCurrentStats.healthLevel].ToString();
        dropCostText.text = dropcosts[playerStatsManager.playerCurrentStats.dropLevel].ToString();
        damageCostText.text = damageCosts[playerStatsManager.playerCurrentStats.damageLevel].ToString();

        if(playerStatsManager.playerCurrentStats.collectedSouls > healthCosts[playerStatsManager.playerCurrentStats.healthLevel - 1]){
            healthButton.interactable = true;
            healthButton.GetComponentInChildren<TMP_Text>().color = Color.green;
            }else{
                healthButton.interactable = false;
                healthButton.GetComponentInChildren<TMP_Text>().color = Color.red;
            }

        if (playerStatsManager.playerCurrentStats.collectedSouls > dropcosts[playerStatsManager.playerCurrentStats.dropLevel - 1])
        {
            dropButton.interactable = true;
            dropButton.GetComponentInChildren<TMP_Text>().color = Color.green;
        }
        else
        {
            dropButton.interactable = false;
            dropButton.GetComponentInChildren<TMP_Text>().color = Color.red;
        }

        if (playerStatsManager.playerCurrentStats.collectedSouls > damageCosts[playerStatsManager.playerCurrentStats.damageLevel - 1])
        {
            damageButton.interactable = true;
            damageButton.GetComponentInChildren<TMP_Text>().color = Color.green;
        }
        else
        {
            damageButton.interactable = false;
            damageButton.GetComponentInChildren<TMP_Text>().color = Color.red;
        }

        }


    public void UpgradeHealth(){    
        print("touched health");
        playerStatsManager.playerCurrentStats.collectedSouls -= healthCosts[playerStatsManager.playerCurrentStats.healthLevel - 1];
        playerStatsManager.playerCurrentStats.healthLevel++;
        initScreen();
    }

    public void UpgradeDrop(){
        playerStatsManager.playerCurrentStats.collectedSouls -= dropcosts[playerStatsManager.playerCurrentStats.dropLevel - 1];
        playerStatsManager.playerCurrentStats.dropLevel++;
        initScreen();
    }

    public void UpgradeDamage(){
        playerStatsManager.playerCurrentStats.collectedSouls -= damageCosts[playerStatsManager.playerCurrentStats.damageLevel - 1];
        playerStatsManager.playerCurrentStats.damageLevel++;
        initScreen();
    }
    
    public void CloseView(){

        gameObject.SetActive(false);
    }

}

public class PowerUpDictionary{

    

}
