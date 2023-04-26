using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{

    public float moveTime = 0.1f;
    public LayerMask collisionLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d;
    private float inverseMoveTime;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / 0.1f;		
    }

    protected bool Move(Vector2Int dir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + dir;

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, collisionLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

    protected virtual void AttemptMove<T>(Vector2Int dir) where T : Component
    {
        RaycastHit2D hit;	
        bool canMove = Move(dir, out hit);

        if (hit.transform == null) return;

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null) OnCantMove(hitComponent);
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2d.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    protected abstract void OnCantMove<T> (T component) where T : Component;

}
