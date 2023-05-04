using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//this class serves as a controller for the logic of the player attacking. 
public class PlayerAttackController : MonoBehaviour
{
    //these variables are needed to check the direction of the current enemy
    [SerializeField] public GameObject target = null;
    [SerializeField] public float distance = 10f;
    [SerializeField] public Vector3 direction;

    //these booleans are needed for detecting if the player is in the attack range
    [SerializeField] public bool hasEnemy = false;
    public static PlayerAttackController Instance = null;

    //weapon prefabs
    [SerializeField] private GameObject macePrefab;
    [SerializeField] private GameObject morningStarPrefab;

    [SerializeField] private MaceTest maceScript;

    //weapon gameobjects
    private GameObject mace;
    private GameObject morningStar;

    private bool hasMace;

    private void Start()
    {
        //singleton instantiating
        if (Instance == null) Instance = this;

        hasMace = true;
        mace = macePrefab;

        LoadWeapon();
    }

    private void LoadWeapon()
    {
        if (hasMace)
        {
            mace.gameObject.SetActive(true);
        }
        else
        {
            //since the morningstar has to rotate around a hinge joint, we assign the player's hinge joint 
            //to the morning star's rigid body
            //this probably is not the best way since the morning star freely rotates around the player and is not fixed to a position
            gameObject.GetComponent<HingeJoint2D>().enabled = true;
            morningStarPrefab.GetComponent<MorningStar>().player = GetComponent<Player>();
            morningStar = Instantiate(morningStarPrefab, gameObject.transform);
        }
    }

    public void switchWeapon()
    {
        if (hasMace)
        {
            mace.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<HingeJoint2D>().enabled = false;
            morningStar.SetActive(false);
        }

        hasMace = !hasMace;

        LoadWeapon();
    }

    //this function handles when an enemy enters the attack range of the player. this function is called
    //on the script attached to the attack range
    public void HasEnemy(GameObject enemy)
    {
        hasEnemy = true;
        target = enemy;
        RotatePlayer();
        maceScript.StartAnimation();
    }

    //this function handles when an enemy exits the attack range of the player. this function is called
    //on the script of the attack range
    public void LostEnemy()
    {
        target = null;
        hasEnemy = false;
        //stops the animation of the mace
        maceScript.StopAnimation();
    }

    private void RotatePlayer()
    {
        maceScript.PauseAnimation();
        
        Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate , 3500 * Time.deltaTime);
        
        maceScript.ResumeAnimation();
    }

    private void Update()
    {   
        if(!hasEnemy) return;
        
        direction = (target.transform.position - transform.position).normalized;
        distance = Vector2.Distance(target.transform.position, transform.position);
        
        //draw the ray in the editor
        Debug.DrawRay(transform.position,direction*distance,Color.red);
    }
    
}
