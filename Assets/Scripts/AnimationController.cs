using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationController : MonoBehaviour
{
    public Animator animator;

    public virtual void FixAndSetAnimation(Vector2 oldDirection){
            //this function is used to snap the animator's blend tree to one fixed animation instead of interpolating between the two closest animations
            //it gets the rotation in respect to the direction to the player and based on the rotation value along the z axis, it snaps to the unique value of each animation

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, oldDirection);

            switch (rotation.eulerAngles.z)
            {
                //N
                case float f when f < 22.5f || f > 337.5f:

                    animator.SetFloat("Horizontal", 0);
                    animator.SetFloat("Vertical", 1);
                    break;

                //NW
                case float f when f >= 22.5f && f < 67.5f:

                    animator.SetFloat("Horizontal", -1);
                    animator.SetFloat("Vertical", 1);
                    break;
                //W
                case float f when f >= 67.5f && f < 112.5f:

                    animator.SetFloat("Horizontal", -1);
                    animator.SetFloat("Vertical", 0);
                    break;
                //SW
                case float f when f >= 112.5f && f < 157.5f:

                    animator.SetFloat("Horizontal", -1);
                    animator.SetFloat("Vertical", -1);
                    break;
                //S
                case float f when f >= 157.5f && f < 202.5f:

                    animator.SetFloat("Horizontal", 0);
                    animator.SetFloat("Vertical", -1);
                    break;
                //SE
                case float f when f >= 202.5f && f < 247.5f:

                    animator.SetFloat("Horizontal", 1);
                    animator.SetFloat("Vertical", -1);
                    break;
                //E
                case float f when f >= 247.5f && f < 292.5f:

                    animator.SetFloat("Horizontal", 1);
                    animator.SetFloat("Vertical", 0);
                    break;
                //NE
                case float f when f >= 292.5f && f < 337.5f:

                    animator.SetFloat("Horizontal", 1);
                    animator.SetFloat("Vertical", 1);
                    break;
            }
        }
    
}
