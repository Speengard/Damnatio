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

    private Animator animator;
    public Transform target;
    [SerializeField] private float speed = 0.3f;

    public int ID;


    private void Start()
    {
        GameManager.Instance.AddEnemyToList(this);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        healthController.SetupHealthBar(); 
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.3f)
        {
            //move if distance from target is greater than 0.3
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x,target.position.y), speed * Time.deltaTime);
        }
    }

}
