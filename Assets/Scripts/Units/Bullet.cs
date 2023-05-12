using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    public Vector2 direction = new Vector2(0, 0);
    public StaticEnemy unit;

    [SerializeField] private GameObject bulletSprite;

    void Start()
    {
        StartCoroutine(SelfDistruct());
        direction = (unit.target.position - transform.position).normalized;


    }
    void Update()
    {

        Vector2 scaledMovement = speed * Time.deltaTime * new Vector2(direction.x, direction.y);
        transform.Translate(scaledMovement);
        //rotate the bullet towards the player
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, (unit.target.transform.position - transform.position).normalized);

        bulletSprite.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3600 * Time.deltaTime);
    }

    private IEnumerator SelfDistruct()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthController>().AddHealth(-unit.damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
