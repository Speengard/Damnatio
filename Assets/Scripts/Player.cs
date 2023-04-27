using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private int damage  = 20;
    [SerializeField] public GameObject target = null;

    [SerializeField] public bool hasEnemy = false;
    
    [SerializeField] public float distance;
    [SerializeField] public Vector3 direction;
    public GameObject bulletPrefab;
    public bool isShooting = false;
    public void ShootAtTarget()
    {
        if (!hasEnemy)
        {
            isShooting = false;
            return;
        }

        if (isShooting)
        {
            return;
        }
        direction = (target.transform.position - transform.position).normalized;
        distance = Vector2.Distance(target.transform.position,transform.position);
        
        //draw the ray in the editor
        Debug.DrawRay(transform.position,direction*distance,Color.red);
        Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>().direction = direction * 0.01f;
        StartCoroutine(Wait());
    }
    
    
    private IEnumerator Wait()
    {
        isShooting = true;
        yield return new WaitForSeconds(1f);
        isShooting = false;
        ShootAtTarget();
    }
    
    private void Update()
    {
        if (isShooting) return;
        ShootAtTarget();
    }
}
