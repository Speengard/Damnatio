using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    /// <summary>
    /// this parent abstract class serves as a base for all the animation controllers of the game. as a default it provides a simple function to fix the animation in 8D based on a given direction vector
    /// </summary>
    /// 
    public Animator animator;

    public virtual void FixAndSetAnimation(Vector2 movementdirection){
            //this function is used to snap the animator's blend tree to one fixed animation instead of interpolating between the two closest animations
            //it gets the rotation in respect to the direction of the the gameobject and based on that value along the z axis, it snaps to the unique value of each animation

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, movementdirection);

            switch (rotation.eulerAngles.z)
            {
            //N
            case float f when f < 22.5f || f > 337.5f:

                rotation = Quaternion.Euler(0, 0, 0);

                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 700 * Time.deltaTime);

                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 1);

                break;

            //NW
            case float f when f >= 22.5f && f < 67.5f:

                rotation = Quaternion.Euler(0, 0, 45);

                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 700 * Time.deltaTime);

                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 1);
                break;
            //W
            case float f when f >= 67.5f && f < 112.5f:

                rotation = Quaternion.Euler(0, 0, 90);

                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 700 * Time.deltaTime);

                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
                break;
            //SW
            case float f when f >= 112.5f && f < 157.5f:

                rotation = Quaternion.Euler(0, 0, 135);

                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 700 * Time.deltaTime);

                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", -1);
                break;
            //S
            case float f when f >= 157.5f && f < 202.5f:

                rotation = Quaternion.Euler(0, 0, 180);

                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 700 * Time.deltaTime);

                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", -1);
                break;
            //SE
            case float f when f >= 202.5f && f < 247.5f:

                rotation = Quaternion.Euler(0, 0, 225);

                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 700 * Time.deltaTime);

                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", -1);
                break;
            //E
            case float f when f >= 247.5f && f < 292.5f:

                rotation = Quaternion.Euler(0, 0, 270);

                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 700 * Time.deltaTime);

                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);
                break;
            //NE
            case float f when f >= 292.5f && f < 337.5f:

                rotation = Quaternion.Euler(0, 0, 315);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 700 * Time.deltaTime);

                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 1);
                break;
            }
        }


    
}
