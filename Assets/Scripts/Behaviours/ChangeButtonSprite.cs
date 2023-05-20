using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSprite : MonoBehaviour
{
    [SerializeField] private Sprite onButtonSprite;
    [SerializeField] private Sprite offButtonSprite;
    [SerializeField] private Sprite onSymbolSprite;
    [SerializeField] private Sprite offSymbolSprite;
    [SerializeField] private Button button;
    [SerializeField] private Image symbol;
    [SerializeField] private bool isOn = true; 

    public void ChangeSprite() {
        if (isOn) {
            button.image.sprite = offButtonSprite;
            symbol.sprite = offSymbolSprite;
            isOn = false;
        } else {
            button.image.sprite = onButtonSprite;
            symbol.sprite = onSymbolSprite;
            isOn = true;
        }
    }
}
