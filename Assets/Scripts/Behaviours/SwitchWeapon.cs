using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWeapon : MonoBehaviour
{
    private PlayerAttackController controller = null;

    [SerializeField] private Sprite morningStarSprite;
    [SerializeField] private Sprite rangedSprite;
    
    public void OnClick()
    {
        if (controller == null) controller = PlayerAttackController.Instance;
        controller.switchWeapon();

        GetComponent<Button>().image.overrideSprite = controller.hasRanged ? morningStarSprite : rangedSprite;
    }
}
