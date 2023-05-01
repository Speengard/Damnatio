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

    [SerializeField] private GameObject mace;
    [SerializeField] private GameObject morningStar;
    private bool hasMace;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        hasMace = true;
        LoadWeapon();
    }

    public void LoadWeapon()
    {
        if (hasMace)
        {
            gameObject.GetComponent<HingeJoint2D>().enabled = true;
            mace = Instantiate(mace).GetComponent<Mace>().player = gameObject;
        }
        else
        {
            gameObject.GetComponent<HingeJoint2D>().enabled = true;
            morningStar = Instantiate(morningStar).GetComponent<MorningStar>().player = gameObject;
        }
    }

    private void Update()
    {
        if (!hasEnemy) return;
        
        //get the direction and the distance from the current enemy
        direction = (target.transform.position - transform.position).normalized;
        distance = Vector2.Distance(target.transform.position,transform.position);
    
        //draw the ray in the editor
        Debug.DrawRay(transform.position,direction*distance,Color.red);
        
    }
    
}
