using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    [Range(0, 5f)] float shootDelay = 5f;
    [Range(0, 7f)] float firstDelay;

    [SerializeField] private Transform spawnPoint;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        firstDelay = Random.Range(0, 7f);
        StartCoroutine(WaitFirstDelay());
        StartCoroutine(ShootCoroutine());
    }

    private void StartAnimation(){
        animator.SetTrigger("Shoot");
    
    }

    private void CloseAnimation(){
        animator.SetTrigger("Shoot");
    }

    private void ShootBullet(){
        //instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().unit = this;
    }

    IEnumerator ShootCoroutine(){
        while(true){
            StartAnimation();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    IEnumerator WaitFirstDelay(){
        yield return new WaitForSeconds(firstDelay);
    }

    IEnumerator WaitForAnimation(){
        yield return new WaitForSeconds(1.2f);
    }

}
