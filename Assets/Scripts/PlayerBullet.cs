using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    public Vector2 direction = new Vector2(0, 0);
    public GameObject Target;
    [SerializeField] private GameObject bulletSprite;

    public void GetTarget(GameObject target){

        
        Target = target;

        //rotate the bullet towards the closest enemy
        Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, (Target.transform.position - transform.position).normalized);

        bulletSprite.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 5000);

        direction = (Target.transform.position - transform.position).normalized;
    }

    private void Update() {
        if(Target != null){
            Vector2 scaledMovement = speed * Time.deltaTime * new Vector2(direction.x, direction.y);
            transform.Translate(scaledMovement);
    }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            print("bullet hit enemy");
            other.gameObject.GetComponent<EnemyHealthController>().AddHealth(-1);
            Destroy(gameObject);
        }
    }
}
