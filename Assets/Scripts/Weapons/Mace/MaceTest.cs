using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceTest : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private int weaponDamage = 20;
    private AnimatorClipInfo[] clipInfo;
    
    //this script handles the animator of the mace and the damage and the delay between swings.
    //ANIMATOR DESCRIPTION:
    //it is really simple, it has 3 states: Idle, Swing.
    //Idle is the default state, it is the state where the mace is not swinging.
    //Swing is the state where the mace is swinging in a loop.
    //from Swing, it can go to idle with the stop trigger.
    //from Idle, it can go to Swing with the swing trigger.
    
    public string GetCurrentClipName(){
        int layerIndex = 0;
        clipInfo = anim.GetCurrentAnimatorClipInfo(layerIndex); 
        return clipInfo[0].clip.name;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy")){
        col.gameObject.GetComponent<EnemyHealthController>().TakeDamage(weaponDamage);
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
        }
    }

    public void StartAnimation()
    {
        print("StartAnimation");
        anim.SetTrigger("Start");   
    }

    public void StopAnimation()
    {
        print("StopAnimation");
        anim.SetTrigger("Stop");
        gameObject.transform.rotation = Quaternion.identity;
    }

    public void PauseAnimation()
    {
        print("Pause");
        anim.speed = 0f;
    }

    public void ResumeAnimation()
    {
        print("Resume");
        anim.speed = 1f;
    }
    
}
