using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SwitchWeapon : MonoBehaviour
{
    private Player player = null;

    [SerializeField] private Sprite morningStarSprite;
    [SerializeField] private Sprite rangedSprite;

    public Slider slider;
    public Image image;
    
    public void OnClick()
    {
        if (player == null)
        {
            player = Player.Instance;
        }

        player.attackController.switchWeapon();

        image.overrideSprite = player.attackController.hasRanged ? morningStarSprite : rangedSprite;

        GetComponent<Button>().interactable = false;
        StartCoroutine(Cooldown(0.0f,() =>
        {
            GetComponent<Button>().interactable = true;
        }));

    }

    IEnumerator Cooldown(float cd,Action callback = null){
        
        while( cd < 2f){
            cd += 0.03f;
            slider.value = cd;
            yield return new WaitForSeconds(0.03f);
        }

        callback?.Invoke();
    }

}
