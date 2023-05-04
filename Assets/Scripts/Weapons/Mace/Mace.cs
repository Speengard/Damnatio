using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : Weapon
{
    public Player player;
    private PlayerAttackController aController;
    private bool hasSwung = false;
    private Animator anim;

    public override void Attack()
    {
        Swing();
    }

    private void LeftToRight()
    {
        
        anim.SetBool("hasSwung", false);
        hasSwung = false;
        StartCoroutine(Wait());
    }

    private void RightToLeft()
    {
        
        anim.SetBool("hasSwung", true);
        hasSwung = true;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        Swing();
    }

    private void Swing()
    {
        if (hasSwung)
        {
            LeftToRight();
        }
        else
        {
            RightToLeft();
        }
    }
    
    private void Start()
    {
        aController = player.attackController;
        anim = GetComponent<Animator>();
        LinkToPlayer();
        Attack();
    }

    public void LinkToPlayer()
    {
        player.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
