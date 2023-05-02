using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    private PlayerAttackController controller = null;
    
    public void OnClick()
    {
        if (controller == null) controller = PlayerAttackController.Instance;
        controller.switchWeapon();
    }
}
