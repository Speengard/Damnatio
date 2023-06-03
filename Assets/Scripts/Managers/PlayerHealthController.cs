using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityCoreHaptics;

public class PlayerHealthController : HealthController
{

    void Start()
    {
        maxHealth = GameManager.Instance.playerStatsManager.playerCurrentStats.maxHealth;
        health = maxHealth;
    }

    override public bool CheckDeath() {
        GameManager.Instance.GameOver();
        return false;
    }

    // make the health bar appear
    public void EnableHealthBar() {
        healthSlider.gameObject.SetActive(true);
    }

    public override void TakeDamage(int damage)
    {
        GameManager.Instance.followPlayer.ShakeCamera();

        if(UnityCoreHaptics.UnityCoreHapticsProxy.SupportsCoreHaptics()){

            UnityCoreHaptics.UnityCoreHapticsProxy.PlayTransientHaptics(0.3f,1);
        }
        
        base.TakeDamage(damage);
    }

}
