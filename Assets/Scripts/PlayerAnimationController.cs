using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    public void switchWeapon(){
        animator.SetTrigger("SwitchWeapon");
    }

    public void FixAnimationAndRotate(Vector2 direction, Transform transformToRotate){

        Quaternion rotation = base.FixAndSetAnimation(direction);

        transformToRotate.rotation = Quaternion.RotateTowards(transformToRotate.rotation, rotation, 700 * Time.deltaTime);

    }
}
