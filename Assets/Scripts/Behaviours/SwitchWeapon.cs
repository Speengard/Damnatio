using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SwitchWeapon : MonoBehaviour
{
    private Player player = null;
    public bool isChanging = false;
    [SerializeField] private Sprite morningStarSprite;
    [SerializeField] private Sprite rangedSprite;

    public Slider slider;
    public Image image;
    
    private void OnEnable() {

        if (player == null)
        {
            player = Player.Instance;
        }
        GetComponent<Button>().interactable = true;

        image.overrideSprite = player.attackController.hasRanged ? morningStarSprite : rangedSprite;

        slider.value = 2f;
    }

    public void OnClick()
    {   
        player.attackController.switchWeapon();

        image.overrideSprite = player.attackController.hasRanged ? morningStarSprite : rangedSprite;

        GetComponent<Button>().interactable = false;
        isChanging = true;

        StartCoroutine(Cooldown(0.0f,() =>
        {
            isChanging = false;
            GetComponent<Button>().interactable = true;
        }));

    }

    IEnumerator Cooldown(float cd,Action callback = null){
        print("started coroutine");
        while( cd < 2f){
            cd += 0.03f;
            slider.value = cd;
            yield return new WaitForSeconds(0.03f);
        }

        callback?.Invoke();
    }

}
