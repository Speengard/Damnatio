using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SwitchWeapon : MonoBehaviour
{
    public bool isChanging = false;
    [SerializeField] private Sprite morningStarSprite;
    [SerializeField] private Sprite rangedSprite;

    public Slider slider;
    public Image image;
    
    private void OnEnable() {

        GetComponent<Button>().interactable = true;
        slider.value = 2f;
    }

    public void OnClick()
    {   
        Player.Instance.attackController.switchWeapon();

        image.overrideSprite = Player.Instance.attackController.hasRanged ? morningStarSprite : rangedSprite;

        GetComponent<Button>().interactable = false;
        isChanging = true;

        StartCoroutine(Cooldown(0.0f,() =>
        {
            isChanging = false;
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
