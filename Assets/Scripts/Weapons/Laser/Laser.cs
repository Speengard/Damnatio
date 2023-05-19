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
    public bool isShooting = false;
    private bool hasHit = false;

    private void OnDisable() {
        DisableLaser();
    }

    private void OnEnable() {
        DisableLaser();
    }

    private void Start() {
        FillList();
        DisableLaser();        
    }

    private void Update() {
        
        if(PlayerAttackController.Instance.target != null) target = PlayerAttackController.Instance.target.transform;
        else target = null;

        if(isShooting) UpdateLaser();
    }

    public void EnableLaser(){
        if(target == null) return;
        if(isShooting) return;

        print("enabling laser");

        isShooting = true;
        lineRenderer.enabled = true;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }

        StartCoroutine(DisableLaserAfterSeconds());
    }
    

    IEnumerator DisableLaserAfterSeconds(){
        yield return new WaitForSeconds(1.5f);
        DisableLaser();
    }

    void UpdateLaser(){

        if(lineRenderer.enabled == false && target == null){
            return;
        } 

        direction = (Vector2) target.position - (Vector2)firePoint.position;

        lineRenderer.SetPosition(0, firePoint.position);
        startVFX.transform.position = (Vector2) firePoint.position;
        lineRenderer.SetPosition(1, firePoint.position * direction.normalized * 3f);

        RaycastHit2D hit = Physics2D.Raycast((Vector2)firePoint.position, direction.normalized, direction.magnitude);

        if(hit){
            if(hit.collider.tag == "Enemy" && !hasHit){
                hasHit = true;
                hit.collider.GetComponent<Enemy>().TakeDamage(1);

                if(hit.collider.GetComponent<HealthController>().CheckDeath()){ 
                    target = null;
                }
            }

            lineRenderer.SetPosition(1, hit.point);
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);
    }

    void DisableLaser(){
        StopAllCoroutines();

        lineRenderer.enabled = false;
        hasHit = false;
        isShooting = false;

        print("disabling laser");

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }

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

    
}