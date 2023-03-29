using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{

    public PlayerUnit playerUnit;
    
    public void updateHUD()
    {
        TMP_Text text = GameObject.FindWithTag("PlayerLifeText").GetComponent<TMP_Text>();
        text.text = playerUnit.currentHP + "/" + playerUnit.maxHP;
        text = GameObject.FindWithTag("PlayerDevotionLevel").GetComponent<TMP_Text>();
        text.text = playerUnit.currentDev + "/" + playerUnit.maxDev;
    }
}
