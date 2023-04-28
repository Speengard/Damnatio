using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    public Vector2 direction;

    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (direction == null) return;
        
        transform.Translate(direction);
    }
    
}
