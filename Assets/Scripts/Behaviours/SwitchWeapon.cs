using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWeapon : MonoBehaviour
{
    private Player player = null;

    [SerializeField] private Sprite morningStarSprite;
    [SerializeField] private Sprite rangedSprite;
    
    public void OnClick()
    {
        if (player == null)
        {
            player = Player.Instance;
        }

        player.attackController.switchWeapon();

        GetComponent<Button>().image.overrideSprite = player.attackController.hasRanged ? morningStarSprite : rangedSprite;
    }
}
