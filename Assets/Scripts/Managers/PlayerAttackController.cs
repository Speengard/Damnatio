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
    public GameObject target = null;
    [SerializeField] public float distance = 10f;
    [SerializeField] public Vector3 direction;
    public bool bIsOnTheMove = false;

    //these booleans are needed for detecting if the player is in the attack range
    public bool hasEnemy = false;

    //weapon prefabs
    [SerializeField] private Laser rangedShoot;
    //weapon gameobjects
    [SerializeField] private GameObject rangedWeapon;
    [SerializeField] private GameObject morningStar;
    public bool hasRanged = false;
    private void Start()
    {
        
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
            morningStar.SetActive(true);
        }
    }

    public void switchWeapon()
    {
        Player.Instance.animationController.switchWeapon();

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



    #region RangedLogic
    //this method is called when the player has no enemy in range and a first enemy enters in the range
    public void FirstEnemy(GameObject firstEnemy)
    {
        hasEnemy = true;
        ChangeTarget(firstEnemy);
        
    }

    //this method is called when the last enemy exits the range
    public void LastEnemy()
    {
        hasEnemy = false;
        ChangeTarget(null);
    }

    //this method is called when the player has an enemy in range and another enemy enters the range but is closer to the player
    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void shootLaser(){
        rangedShoot.EnableLaser();
    }

    
    private void Update()
    {

        if(hasEnemy && hasRanged){
        //check if the player is moving
        if(!rangedShoot.isShooting)StartCoroutine(CheckMoving());
        

        direction = (target.transform.position - transform.position).normalized;
        distance = Vector2.Distance(target.transform.position, transform.position);

        //draw the ray in the editor
        Debug.DrawRay(transform.position,direction*distance,Color.red);

        }
    }


    private IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(1.5f);
        Vector3 finalPos = transform.position;

        if (startPos.x != finalPos.x || startPos.y != finalPos.y
            || startPos.z != finalPos.z)
            bIsOnTheMove = true;

            else{
                if (!rangedShoot.isShooting) shootLaser();
                bIsOnTheMove = false;
            } 
    }
    #endregion

}
