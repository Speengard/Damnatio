using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//this class serves as a controller for the logic of the player attacking. 
public class PlayerAttackController : MonoBehaviour
{
    //these variables are needed to check the direction of the current enemy
    [SerializeField] public GameObject target = null;
    [SerializeField] public float distance;
    [SerializeField] public Vector3 direction;
    
    //these booleans are needed for detecting if the player is in the attack range
    [SerializeField] public bool hasEnemy = false;
    public static PlayerAttackController Instance = null;

    [SerializeField] private GameObject macePrefab;
    [SerializeField] private GameObject morningStarPrefab;

    private GameObject mace;
    private GameObject morningStar;
    
    private bool hasMace;
    
    private void Start()
    {
        if (Instance == null) Instance = this;
        hasMace = true;
        LoadWeapon();
    }

    private void LoadWeapon()
    {
        if (hasMace)
        {
            gameObject.GetComponent<HingeJoint2D>().enabled = true;
            macePrefab.GetComponent<Mace>().player = GetComponent<Player>();
            mace = Instantiate(macePrefab,gameObject.transform);
        }
        else
        {
            gameObject.GetComponent<HingeJoint2D>().enabled = true;
            morningStarPrefab.GetComponent<MorningStar>().player = GetComponent<Player>();
            morningStar = Instantiate(morningStarPrefab,gameObject.transform);
        }
    }

    public void switchWeapon()
    {
        if (hasMace)
        {
            gameObject.GetComponent<HingeJoint2D>().enabled = false;
            mace.SetActive(false);
        }
        else
        {gameObject.GetComponent<HingeJoint2D>().enabled = false;
            morningStar.SetActive(false);
        }

        hasMace = !hasMace;
        
        LoadWeapon();
    }

    private void Update()
    {
        if (!hasEnemy) return;
        
        //get the direction and the distance from the current enemy
        direction = (target.transform.position - transform.position).normalized;
        distance = Vector2.Distance(target.transform.position,transform.position);

        Quaternion toRotate = Quaternion.LookRotation(Vector3.forward,direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 1000 * Time.deltaTime);

        //draw the ray in the editor
        Debug.DrawRay(transform.position,direction*distance,Color.red);
        
    }
    
}
