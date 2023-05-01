using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningStar : Weapon
{

    public GameObject player;
    [SerializeField] private GameObject finalLink;
    
    public override void Attack()
    {

    }

    private void Start()
    {
        LinkToPlayer();
    }

    public void LinkToPlayer()
    {
        player.GetComponent<HingeJoint2D>().connectedBody = finalLink.GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
    
    
}
