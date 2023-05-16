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
    [SerializeField] private Transform target;
    private Vector2 direction;
    private bool isShooting = false;
    private bool hasHit = false;

    private void Start() {
        FillList();
        DisableLaser();        
    }

    private void Update() {
        if(isShooting) UpdateLaser();
    }

    public void EnableLaser(Transform target){
        if (target == null) return;

        this.target = target;
        StartCoroutine(EnableAfterDelay());
    }
    
    IEnumerator EnableAfterDelay(){
        yield return new WaitForSeconds(0.7f);
        
        lineRenderer.enabled = true;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }

        isShooting = true;
        StartCoroutine(DisableLaserAfterSeconds());
    }

    IEnumerator DisableLaserAfterSeconds(){
        yield return new WaitForSeconds(0.7f);
        DisableLaser();
        isShooting = false;
    }

    void UpdateLaser(){

        if(target == null) DisableLaser();

        direction = (Vector2) target.position - (Vector2)firePoint.position;

        lineRenderer.SetPosition(0, firePoint.position);
        startVFX.transform.position = (Vector2) firePoint.position;
        lineRenderer.SetPosition(1, firePoint.position * direction.normalized * 3f);

        //RotateLaser();

        RaycastHit2D hit = Physics2D.Raycast((Vector2)firePoint.position, direction.normalized, direction.magnitude);

        if(hit){
            if(hit.collider.tag == "Enemy" && !hasHit){
                hasHit = true;
                hit.collider.GetComponent<Enemy>().TakeDamage(1);
            }

            lineRenderer.SetPosition(1, hit.point);
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);
    }

    void DisableLaser(){
        lineRenderer.enabled = false;
        hasHit = false;
        isShooting = false;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
        StartCoroutine(WaitForDelayThenShoot());
    }

    void RotateLaser(){
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = rotation;
    }

    void FillList(){

        for(int i = 0; i<startVFX.transform.childCount; i++){
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null){
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

    IEnumerator WaitForDelayThenShoot()
    {
        yield return new WaitForSeconds(3f);
        EnableLaser(target);
    }
}