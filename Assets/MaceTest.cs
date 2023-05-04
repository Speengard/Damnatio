using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceTest : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float delay = 0.75f;
    
    //this script handles the animator of the mace and the damage and the delay between swings.
    //ANIMATOR DESCRIPTION:
    //from every state, it can go back to the idle state with the Stop trigger.
    //from the idle state, you enter with the Start trigger, in right to left swing and from there
    //you can enter the left to right swing with the Swing trigger. then back to the right to left once again
    //with the Swing trigger, creating a loop.
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        print("collision mace");
    }

    public void StartAnimation()
    {
        print("start");
        anim.SetTrigger("Start");
        Swing();   
    }

    public void StopAnimation()
    {
        print("stop");
        anim.SetTrigger("Stop");
    }

    private void Swing()
    {
        print("swing");
        anim.SetTrigger("Swing");
    }

    public void PauseAnimation()
    {
        anim.speed = 0f;
    }

    public void ResumeAnimation()
    {
        anim.speed = 1f;
    }
    
}
