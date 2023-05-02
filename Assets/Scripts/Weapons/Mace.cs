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
        print("leftToRight");
        anim.SetBool("hasSwung", false);
        hasSwung = false;
        StartCoroutine(Wait());
    }

    private void RightToLeft()
    {
        print("rightToLeft");
        anim.SetBool("hasSwung", true);
        hasSwung = true;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
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

    private void GetDirection()
    {
        if(aController.hasEnemy) return;
        Attack();
    }

    private void Start()
    {
        aController = player.attackController;
        anim = GetComponent<Animator>();
        LinkToPlayer();
        GetDirection();
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
