using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int damage; // damage that the enemy does on the player
    public Slider healthSlider;
    [SerializeField] private EnemyHealthController healthController;
    [SerializeField] private Animator enemyAnimator;

    private Animator animator;
    public Transform target;
    [SerializeField] private float speed = 0.3f;
    private Vector3 direction;
    public int ID;


    private void Start()
    {
        GameManager.Instance.AddEnemyToList(this);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        healthController.SetupHealthBar(); 
    
    }

    private void FixedUpdate()
    {

        direction = (target.position - transform.position).normalized;
        enemyAnimator.SetFloat("Horizontal", direction.x);
        enemyAnimator.SetFloat("Vertical", direction.y);

        if (Vector2.Distance(transform.position, target.position) > 0.3f)
        {
            //move if distance from target is greater than 0.3
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x,target.position.y), speed * Time.deltaTime);
        }
    }

}
