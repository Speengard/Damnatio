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
    public bool bIsOnTheMove = false;

    //these booleans are needed for detecting if the player is in the attack range
    [SerializeField] public bool hasEnemy = false;
    public static PlayerAttackController Instance = null;
    //weapon prefabs
    [SerializeField] private Laser rangedShoot;
    //weapon gameobjects
    [SerializeField] private GameObject rangedWeapon;
    [SerializeField] private GameObject morningStar;
    public bool hasRanged = false;
    private void Start()
    {
        //singleton instantiating
        if (Instance == null) Instance = this;
        LoadWeapon();
    }

    private void LoadWeapon()
    {
        if (hasRanged)
        {
            rangedWeapon.gameObject.SetActive(true);
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
        if (hasRanged)
        {
            
            rangedWeapon.SetActive(false);
            
        }
        else
        {
            morningStar.SetActive(false);   
        }

        hasRanged = !hasRanged;
        LoadWeapon();
    }



    #region maceLogic
    //this method is called when the player has no enemy in range and a first enemy enters in the range
    public void FirstEnemy(GameObject firstEnemy)
    {
        if (!hasRanged) return;
        ChangeTarget(firstEnemy);
        
    }

    //this method is called when the last enemy exits the range
    public void LastEnemy()
    {
        if (!hasRanged) return;
        hasEnemy = false;
        target = null;
    }
    //this method is called when an enemy moves further away enough in order to rotate towards it or when the first enemy enters the range
    private void RotatePlayerTowardsEnemy()
    {
        Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, direction);
        
        //this line below is used to fix the animator on one of the coordinates, the same way we used to fix the enemy's animation
        //GetComponent<PlayerMovementController>().RotateTowards(toRotate);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 700 * Time.deltaTime);

           
        if(target != null && bIsOnTheMove) rangedShoot.EnableLaser(target.GetComponent<Transform>());
        
    }

    //this method is called when the player has an enemy in range and another enemy enters the range but is closer to the player
    public void ChangeTarget(GameObject newTarget)
    {
        if (!hasRanged) return;

        target = newTarget;
        hasEnemy = true;

        RotatePlayerTowardsEnemy();
    }

    #endregion
    
    private void Update()
    {

        StartCoroutine(CheckMoving());
       if(hasEnemy && target != null){   

                direction = (target.transform.position - transform.position).normalized;
                distance = Vector2.Distance(target.transform.position, transform.position);
       }    

        //draw the ray in the editor
        Debug.DrawRay(transform.position,direction*distance,Color.red);
    }

    private IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(0.5f);
        Vector3 finalPos = transform.position;
        if (startPos.x != finalPos.x || startPos.y != finalPos.y
            || startPos.z != finalPos.z)
            bIsOnTheMove = true;
    }

}
