using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MovingObject
{
    public int playerDamage;

    private Animator animator;
    public Transform target;
    private bool skipMove;
    private float speed = 1f;

    public AudioClip[] attackClips;

    protected override void Start()
    {
        //GameManager.Instance.AddEnemyToList(this);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        print(target);
        base.Start();
    }

    private void Update()
    {

        if (Vector3.Distance(transform.position, target.position) > 0.3f)
        {
            //move if distance from target is greater than 1
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x,target.position.y), speed * Time.deltaTime);
        }   
    }

    public void MoveEnemy()
    {
        Vector2Int dir = Vector2Int.zero;
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            dir.y = target.position.y > transform.position.y ? 1 : -1;
        else
            dir.x = target.position.x > transform.position.x ? 1 : -1;

        AttemptMove<Player>(dir);
    }

    protected override void AttemptMove<T>(Vector2Int dir)
    {
        
    }

    protected override void OnCantMove<T>(T component)
    {
        
    }
}
