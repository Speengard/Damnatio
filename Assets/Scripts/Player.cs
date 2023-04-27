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
    public void DrawLineToEnemy()
    {
        Vector3 start = transform.position;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        distance = Vector2.Distance(target.transform.position,transform.position);
        
        //draw the ray in the editor
        Debug.DrawRay(start,direction*distance,Color.red);
    }
    
    private void Update()
    {
        if (!hasEnemy) return;
        DrawLineToEnemy();
    }
}
