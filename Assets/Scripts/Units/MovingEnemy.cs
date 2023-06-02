using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{

    //this class serves as a parent class to any enemy who attacks in melee and moves toward the player to attack him. Since the animation tree interpolates animations, a method is needed to fix the current animation to a single one. The blend tree uses two values Horizontalinput and Verticalinput to determine the direction of the enemy. The direction is calculated by the direction vector between the enemy and the player. The direction vector is normalized and then used to calculate the rotation of the enemy. Based on the rotation, the blend tree is snapped to a single animation instead of interpolating between the two closest animations.    
    [SerializeField] private float speed = 0.3f;
    private Vector2 oldDirection;
    [SerializeField] private AnimationController animationController;
    public Quaternion rotation;
    private Enemy enemy;

    private void Awake() {
        enemy = GetComponent<Enemy>();
    }
    private void Update() {
    
        Vector2 direction = (target.position - transform.position).normalized;

        if(oldDirection != direction){

            oldDirection = direction;
            animationController.FixAndSetAnimation(direction);
            
        }

        oldDirection = direction;

        if (Vector2.Distance(transform.position, target.position) > 0.3f && !enemy.stopMoving)
        {
            //move if distance from target is greater than 0.3
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x,target.position.y), speed * Time.deltaTime);
        }
    }
}
