using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    public Transform firePoint;
    private Quaternion rotation;
    [SerializeField] private GameObject startVFX;
    [SerializeField] private GameObject endVFX;
    private List<ParticleSystem> particles = new List<ParticleSystem>();
    [SerializeField] private Vector3 target;
    private Vector2 direction;
    public bool isShooting = false;
    private bool hasHit = false;
    private int rangedDamage = 5;

    private void OnDisable()
    {
        hasHit = false;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }


    }

    private void OnEnable()
    {
        hasHit = false;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }


    }

    private void Awake()
    {
        FillList();
       DisableLaser();
    }

    private void Update()
    {
        UpdateLaser();
    }

    public void EnableLaser( float laserWidth, GameObject target)
    {
        if (target == null) return;
        if (isShooting) return;

        switch(laserWidth){
            case <= 0.5f:
                rangedDamage = 5;
                break;
            case <= 1f:
                rangedDamage = 7;
                break;
            case <= 1.5f:
                rangedDamage = 9;
                break;
            case <= 2f:
            rangedDamage = 15;
                break;
            default:
                rangedDamage = 5;
                break;
        }

        this.target = target.transform.position;
        
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;

        lineRenderer.enabled = true;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }

        isShooting = true;
        StartCoroutine(DisableLaserAfterSeconds());
    }


    IEnumerator DisableLaserAfterSeconds()
    {
        yield return new WaitForSeconds(1f);
        DisableLaser();
    }

    void UpdateLaser()
    {
        if(lineRenderer.enabled == false) return;
        lineRenderer.SetPosition(0, firePoint.position);
        startVFX.transform.position = (Vector2)firePoint.position;
        direction = (Vector2)target - (Vector2)firePoint.position;
        lineRenderer.SetPosition(1, firePoint.position * direction.normalized * 3f);

        RaycastHit2D hit = Physics2D.Raycast((Vector2)firePoint.position, direction.normalized, direction.magnitude);

        if (hit)
        {
            if (hit.collider.tag == "Enemy" && !hasHit)
            {
                hasHit = true;
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(rangedDamage);
            }

            lineRenderer.SetPosition(1, hit.point);
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);
    }

    private void DisableLaser()
    {
        //stop particle effects
        for(int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }

        lineRenderer.enabled = false;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        hasHit = false;

        StartCoroutine(WaitCooldown());
    }

    IEnumerator WaitCooldown(){
        yield return new WaitForSeconds(3f);
        isShooting = false;
    }

    public void StopEverything(){
        StopAllCoroutines();
        //stop particle effects
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }

        lineRenderer.enabled = false;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;

    }
    void FillList()
    {

        for (int i = 0; i < startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }

        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
    }


}