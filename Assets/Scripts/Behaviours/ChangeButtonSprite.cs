using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSprite : MonoBehaviour
{
    [SerializeField] private Sprite newButtonSprite;
    [SerializeField] private Button button;

    public void ChangeSprite() {
        button.image.sprite = newButtonSprite;
    }
}
