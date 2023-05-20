using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    public void switchWeapon(){
        animator.SetTrigger("SwitchWeapon");
    }
}
