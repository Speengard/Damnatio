using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningStar : Weapon
{
    public GameObject bulletPrefab;
    public bool isShooting = false;

    private void Start()
    {
        delay = 1f;
    }

    //this function is the shoot logic, the bullet is a prefab which we instantiate and give a direction in order to let the bullet fly
    public override void Attack()
    {
        if (!attackController.hasEnemy)
        {
            isShooting = false;
            return;
        }

        if (isShooting)
        {
            return;
        }

        if (attackController.hasEnemy == false)
        {
            return;
        }
        
        //shoot at the enemy
        Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>().direction = attackController.direction * 0.01f;
        StartCoroutine(Wait());
    }
    
    //this function serves as a delay between attacks, in order to increase or decrease, all is needed is to change the
    //argument of the WaitForSeconds function call
    private IEnumerator Wait()
    {
        isShooting = true;
        yield return new WaitForSeconds(delay);
        isShooting = false;
        Attack();
    }
}
