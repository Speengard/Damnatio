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
    [SerializeField] private EnemyHealthController healthController;

    public Quaternion rotation;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        healthController.SetupHealthBar();
    }

    private void Update() {
    
        direction = (target.position - transform.position).normalized;
        FixAndSetAnimation();

        if (Vector2.Distance(transform.position, target.position) > 0.3f)
        {
            //move if distance from target is greater than 0.3
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x,target.position.y), speed * Time.deltaTime);
        }
    }

    private void FixAndSetAnimation(){
        //this function is used to snap the animator's blend tree to one fixed animation instead of interpolating between the two closest animations
        //it gets the rotation in respect to the direction to the player and based on the rotation value along the z axis, it snaps to the unique value of each coordinate
        rotation = Quaternion.LookRotation(Vector3.forward, direction);

        switch(rotation.eulerAngles.z){
            //N
            case float f when f< 22.5f || f > 337.5f:
               
                 enemyAnimator.SetFloat("Horizontal", 0);
                 enemyAnimator.SetFloat("Vertical", 1);
                break;
            
            //NW
            case float f when f>=22.5f && f<67.5f:
                
                 enemyAnimator.SetFloat("Horizontal", -1);
                 enemyAnimator.SetFloat("Vertical", 1);
                break;
            //W
            case float f when f>=67.5f && f<112.5f:
                
                 enemyAnimator.SetFloat("Horizontal", -1);
                 enemyAnimator.SetFloat("Vertical", 0);
                break;
            //SW
            case float f when f>=112.5f && f<157.5f:
                
                 enemyAnimator.SetFloat("Horizontal", -1);
                 enemyAnimator.SetFloat("Vertical", -1);
                break;
            //S
            case float f when f>=157.5f && f<202.5f:
                
                 enemyAnimator.SetFloat("Horizontal", 0);
                 enemyAnimator.SetFloat("Vertical", -1);
                break;
            //SE
            case float f when f>=202.5f && f<247.5f:
               
                 enemyAnimator.SetFloat("Horizontal", 1);
                 enemyAnimator.SetFloat("Vertical", -1);
                break;
            //E
            case float f when f>=247.5f && f<292.5f:
                
                 enemyAnimator.SetFloat("Horizontal", 1);
                 enemyAnimator.SetFloat("Vertical", 0);
                break;
            //NE
            case float f when f>=292.5f && f<337.5f:
                
                enemyAnimator.SetFloat("Horizontal", 1);
                enemyAnimator.SetFloat("Vertical", 1);
                break;
        }
    }
}
