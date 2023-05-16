using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    [Range(0, 5f)] float shootDelay = 5f;
    [Range(0, 7f)] float firstDelay;

    public bool isShooting = false;
    
    public GameObject target;
    [SerializeField] private Transform spawnPoint;
    // Start is called before the first frame update

    public void Shoot(GameObject target)
    {
        if(isShooting) return;
        this.target = target;
        StartCoroutine(ShootCoroutine());
    }

    private void StartAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    private void StopAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    private void ShootBullet()
    {
        if(target == null) return;
        //instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        bullet.GetComponent<PlayerBullet>().GetTarget(target);

    }

    

    IEnumerator ShootCoroutine()
    {
        isShooting = true;
        while (true)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    public void StopShooting()
    {
        isShooting = false;
        StopCoroutine(ShootCoroutine());
    }
}
