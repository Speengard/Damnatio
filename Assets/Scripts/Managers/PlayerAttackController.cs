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

        hasMace = false;
        mace = macePrefab;
        morningStar = morningStarPrefab; 
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
            morningStar.SetActive(true);
        }
    }

    public void switchWeapon()
    {
        if (hasMace)
        {
            maceScript.StopAnimation();
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

    public void FirstEnemy(GameObject firstEnemy){
        ChangeTarget(firstEnemy);
        maceScript.StartAnimation();
    }

    public void LastEnemy(){
        hasEnemy = false;
        target = null;
        maceScript.StopAnimation();
    }

    private void RotatePlayer()
    {
        maceScript.PauseAnimation();

        Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate , 2000 * Time.deltaTime);

        maceScript.ResumeAnimation();
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
        hasEnemy = true;

        maceScript.PauseAnimation();
        RotatePlayer();
        maceScript.ResumeAnimation();
    }
    
    private void Update()
    {
       if(hasEnemy){
        if(target != null){
                direction = (target.transform.position - transform.position).normalized;
                distance = Vector2.Distance(target.transform.position, transform.position);

                if (distance > 1f)
                {
                    RotatePlayer();
                }
        }
       }    

        //draw the ray in the editor
        Debug.DrawRay(transform.position,direction*distance,Color.red);
    }

}
