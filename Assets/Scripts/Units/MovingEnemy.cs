using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEnemy : MonoBehaviour
{
    public LayerMask collisionLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d;

    public Transform target;
    [SerializeField] private float speed = 0.3f;
    private Vector3 direction;
    [SerializeField] private Animator enemyAnimator;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        direction = (target.position - transform.position).normalized;
        enemyAnimator.SetFloat("Horizontal", direction.x);
        enemyAnimator.SetFloat("Vertical", direction.y);

        if (Vector2.Distance(transform.position, target.position) > 0.3f)
        {
            //move if distance from target is greater than 0.3
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x,target.position.y), speed * Time.deltaTime);
        }
    }
}
