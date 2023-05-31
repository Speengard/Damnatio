using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//this class serves as a controller for the logic of the player attacking. 
public class PlayerAttackController : MonoBehaviour
{
    //these variables are needed to check the direction of the current enemy
    public GameObject target = null;
    [SerializeField] public float distance = 10f;
    [SerializeField] public Vector3 direction;


    //these booleans are needed for detecting if the player is in the attack range
    public bool hasEnemy = false;

    //weapon prefabs
    [SerializeField] private Laser rangedShoot;
    //weapon gameobjects
    [SerializeField] private GameObject rangedWeapon;
    [SerializeField] private GameObject morningStar;
    public bool hasRanged = false;
    public float laserWidth = 0.5f;
    private bool isChecking = false;

    public float morningStarDamage;
    public float rangedDamage;

    public GameObject laserSlider;

    private float counter = 0.0f;
    
    private void Start()
    {
        laserSlider = GameManager.Instance.laserSlider;
        LoadWeapon();
    }

    private void LoadWeapon()
    {
        if (hasRanged)
        {
            laserSlider.SetActive(true);
            rangedWeapon.gameObject.SetActive(true);
        }
        else
        {
            laserSlider.SetActive(false);
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
        rangedShoot.EnableLaser(counter, target);
    }

    
    private void Update()
    {

        if(hasRanged){
        //check if the player is moving
        if(!rangedShoot.isShooting && !isChecking) StartCoroutine(CheckMoving());
        laserSlider.GetComponent<Slider>().value = counter;
        
        if(target != null){

        direction = (target.transform.position - transform.position).normalized;
        distance = Vector2.Distance(target.transform.position, transform.position);

        //draw the ray in the editor
        Debug.DrawRay(transform.position,direction*distance,Color.red);

        }

        }
    }


    private IEnumerator CheckMoving()
    {
        print("checking");

        isChecking = true;

        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector3 finalPos = transform.position;

        if (!startPos.Equals(finalPos)){

            if (!rangedShoot.isShooting && rangedWeapon.activeSelf) shootLaser();
            counter = 0.0f;
            isChecking = false;
            yield break;

            }

            else{

            if (counter < 2.0f)
            {
                counter += 0.1f;
            }

                isChecking = false;
                yield break;
            } 
    }
    
    #endregion

}
