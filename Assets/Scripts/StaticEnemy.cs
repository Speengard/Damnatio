using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    [Range(0, 3f)] float shootDelay = 4f;
    [Range(0, 7f)] float firstDelay;

    [SerializeField] private Transform spawnPoint;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        target = GameObject.FindObjectOfType<Player>().transform;
        firstDelay = Random.Range(0, 7f);
        StartCoroutine(WaitFirstDelay());
        StartCoroutine(ShootCoroutine());
    }

    private void ShootToPlayer(){
        animator.SetTrigger("Shoot");
        StartCoroutine(WaitForAnimation());

        //rotate the bullet towards the player
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, (target.transform.position - transform.position).normalized);
        //instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position,Quaternion.identity);
        
        bullet.GetComponent<Bullet>().unit = this;
        
        animator.SetTrigger("Shoot");

    }

    IEnumerator ShootCoroutine(){
        while(true){
            ShootToPlayer();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    IEnumerator WaitFirstDelay(){
        yield return new WaitForSeconds(firstDelay);
    }

    IEnumerator WaitForAnimation(){
        yield return new WaitForSeconds(1f);
    }

}
