using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : Weapon
{

    public GameObject player;
    public override void Attack()
    {
        
    }
    
    private void Start()
    {
        LinkToPlayer();
    }

    public void LinkToPlayer()
    {
        player.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0,0,0.1f));
    }
    
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
