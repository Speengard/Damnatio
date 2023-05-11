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
    [SerializeField] private MaceTest maceScript;
    //weapon gameobjects
    [SerializeField] private GameObject mace;
    [SerializeField] private GameObject morningStar;
    [SerializeField]private bool hasMace;

    private void Start()
    {
        //singleton instantiating
        if (Instance == null) Instance = this;
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
            morningStar.SetActive(false);   
        }

        hasMace = !hasMace;
        LoadWeapon();
    }



    #region maceLogic
    //this method is called when the player has no enemy in range and a first enemy enters in the range
    public void FirstEnemy(GameObject firstEnemy)
    {
        if (!hasMace) return;
        ChangeTarget(firstEnemy);
        maceScript.StartAnimation();
    }

    //this method is called when the last enemy exits the range
    public void LastEnemy()
    {
        if (!hasMace) return;
        hasEnemy = false;
        target = null;
        maceScript.StopAnimation();
    }
    //this method is called when an enemy moves further away enough in order to rotate towards it or when the first enemy enters the range
    private void RotatePlayer()
    {
        if (!hasMace) return;
        maceScript.PauseAnimation();

        Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate , 2000 * Time.deltaTime);

        maceScript.ResumeAnimation();
    }

    //this method is called when the player has an enemy in range and another enemy enters the range but is closer to the player
    public void ChangeTarget(GameObject newTarget)
    {
        if (!hasMace) return;
        target = newTarget;
        hasEnemy = true;

        maceScript.PauseAnimation();
        RotatePlayer();
        maceScript.ResumeAnimation();
    }

    #endregion
    
    private void Update()
    {
       if(hasEnemy){   

        if(target != null && hasMace){
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
