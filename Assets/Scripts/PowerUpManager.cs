using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public List<SpriteRenderer> healthCapsules = new List<SpriteRenderer>();
    public List<SpriteRenderer> dropCapsules = new List<SpriteRenderer>();
    public List<SpriteRenderer> meleeCapsules = new List<SpriteRenderer>();
    public List<SpriteRenderer> rangedCapsules = new List<SpriteRenderer>();

    public TMP_Text healthText;
    public TMP_Text dropText;
    public TMP_Text meleeText;
    public TMP_Text rangedText;

    public TMP_Text healthCostText;
    public TMP_Text dropCostText;
    public TMP_Text meleeCostText;
    public TMP_Text rangedCostText;

    public Button healthButton;
    public Button dropButton;
    public Button meleeButton;
    public Button rangedButton;
    PlayerStatsManager playerStatsManager;
    public Sprite fullCapsule;

    public int[] healthCosts = { 0, 150, 450, 750, 1050, 1500 };
    public int[] damageCosts = { 0, 150, 450, 750, 1050, 1500 };
    public int[] dropcosts = { 0, 150, 450, 750, 1500, 4000 };

    public GameObject pauseButton;
    public GameObject floatingJoystick;
    public ShowPowerUp showPowerUp;

    private void OnEnable()
    {
        playerStatsManager = GameManager.Instance.playerStatsManager;
        Player.Instance.movementController.enabled = false;
        showPowerUp.canShow = false;
        pauseButton.SetActive(false);
        floatingJoystick.SetActive(false);
        initScreen();
    }

    private void OnDisable()
    {
        Player.Instance.movementController.enabled = true;

        floatingJoystick.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.saveStats();
    }


    private void initScreen()
    {
        healthText.text = "Health: - level " + playerStatsManager.playerCurrentStats.healthLevel;
        dropText.text = "Drop: - level " + playerStatsManager.playerCurrentStats.dropLevel;
        meleeText.text = "Melee: - level " + playerStatsManager.playerCurrentStats.meleeLevel;
        rangedText.text = "Ranged: - level " + playerStatsManager.playerCurrentStats.rangedLevel;

        for (int i = 0; i < playerStatsManager.playerCurrentStats.healthLevel; i++)
        {
            healthCapsules[i].sprite = fullCapsule;
        }
        for (int i = 0; i < playerStatsManager.playerCurrentStats.dropLevel; i++)
        {
            dropCapsules[i].sprite = fullCapsule;
        }
        for (int i = 0; i < playerStatsManager.playerCurrentStats.meleeLevel; i++)
        {
            meleeCapsules[i].sprite = fullCapsule;
        }
        for (int i = 0; i < playerStatsManager.playerCurrentStats.rangedLevel; i++)
        {
            rangedCapsules[i].sprite = fullCapsule;
        }

        healthCostText.text = healthCosts[playerStatsManager.playerCurrentStats.healthLevel].ToString();
        dropCostText.text = dropcosts[playerStatsManager.playerCurrentStats.dropLevel].ToString();
        meleeCostText.text = damageCosts[playerStatsManager.playerCurrentStats.meleeLevel].ToString();
        rangedCostText.text = damageCosts[playerStatsManager.playerCurrentStats.rangedLevel].ToString();

        if (playerStatsManager.playerCurrentStats.collectedSouls > healthCosts[playerStatsManager.playerCurrentStats.healthLevel])
        {
            healthButton.interactable = true;
            healthButton.GetComponentInChildren<TMP_Text>().color = Color.green;
        }
        else
        {
            healthButton.interactable = false;
            healthButton.GetComponentInChildren<TMP_Text>().color = Color.red;
        }

        if (playerStatsManager.playerCurrentStats.collectedSouls > dropcosts[playerStatsManager.playerCurrentStats.dropLevel])
        {
            dropButton.interactable = true;
            dropButton.GetComponentInChildren<TMP_Text>().color = Color.green;
        }
        else
        {
            dropButton.interactable = false;
            dropButton.GetComponentInChildren<TMP_Text>().color = Color.red;
        }

        if (playerStatsManager.playerCurrentStats.collectedSouls > damageCosts[playerStatsManager.playerCurrentStats.meleeLevel])
        {
            meleeButton.interactable = true;
            meleeButton.GetComponentInChildren<TMP_Text>().color = Color.green;
        }
        else
        {
            meleeButton.interactable = false;
            meleeButton.GetComponentInChildren<TMP_Text>().color = Color.red;
        }

        if (playerStatsManager.playerCurrentStats.collectedSouls > damageCosts[playerStatsManager.playerCurrentStats.rangedLevel])
        {
            rangedButton.interactable = true;
            rangedButton.GetComponentInChildren<TMP_Text>().color = Color.green;
        }
        else
        {
            rangedButton.interactable = false;
            rangedButton.GetComponentInChildren<TMP_Text>().color = Color.red;
        }

    }


    public void UpgradeHealth()
    {

        playerStatsManager.playerCurrentStats.collectedSouls -= healthCosts[playerStatsManager.playerCurrentStats.healthLevel];
        playerStatsManager.playerCurrentStats.healthLevel++;
        playerStatsManager.BuyPowerUp("health");
        initScreen();
    }

    public void UpgradeDrop()
    {
        playerStatsManager.playerCurrentStats.collectedSouls -= dropcosts[playerStatsManager.playerCurrentStats.dropLevel];
        playerStatsManager.playerCurrentStats.dropLevel++;
        playerStatsManager.BuyPowerUp("drop");
        initScreen();
    }

    public void UpgradeMeleeDamage()
    {
        playerStatsManager.playerCurrentStats.collectedSouls -= damageCosts[playerStatsManager.playerCurrentStats.meleeLevel];
        playerStatsManager.playerCurrentStats.meleeLevel++;
        playerStatsManager.BuyPowerUp("melee");
        initScreen();
    }

    public void UpgradeRangedDamage()
    {
        playerStatsManager.playerCurrentStats.collectedSouls -= damageCosts[playerStatsManager.playerCurrentStats.rangedLevel];
        playerStatsManager.playerCurrentStats.rangedLevel++;
        playerStatsManager.BuyPowerUp("ranged");
        initScreen();
    }

    public void CloseView()
    {
        CanvasManager.Instance.isShowingPowerUp = false;
        
        if(CanvasManager.Instance.flag == false){
            CanvasManager.Instance.flag = true;
        }
        pauseButton.SetActive(true);
        gameObject.SetActive(false);
    }

}
