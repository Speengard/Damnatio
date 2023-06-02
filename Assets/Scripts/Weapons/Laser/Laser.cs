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
    [SerializeField] public GameObject target;
    private Vector2 direction;
    public bool isShooting = false;
    private bool hasHit = false;
    private int rangedDamage = 3;
    [SerializeField] private Material laserMaterial;

    private void Start() {
        rangedDamage = Player.Instance.runStats.playerCurrentStats.rangedDamage;
    }
    private void OnDisable()
    {
        hasHit = false;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
        StopEverything();


    }

    private void OnEnable()
    {
        hasHit = false;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
        StopEverything();

    }

    private void Awake()
    {
        FillList();
        DisableLaser();
    }

    private void Update()
    {
        if(lineRenderer.enabled) UpdateLaser();
    }

    public void EnableLaser( float laserWidth, GameObject target)
    {
        if (target == null) return;
        if (isShooting) return;

        switch(laserWidth){
            case <= 0.5f:
                rangedDamage += 0;
                break;
            case <= 1f:
                rangedDamage += 5;
                break;
            case <= 1.5f:
                rangedDamage += 7;
                break;
            case <= 2f:
            laserMaterial.SetFloat("_LaserEdgeThickness",7.6f);
            rangedDamage += 10;
                break;
            case > 2f:
                laserMaterial.SetFloat("_LaserEdgeThickness", 7.6f);
                rangedDamage += 10;
                break;
            default:
                rangedDamage += 0;
                break;
        }

        this.target = target;
        
        direction = (Vector2)target.transform.position - (Vector2)firePoint.position;

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, direction.normalized);
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
        direction = (Vector2)target.transform.position - (Vector2)firePoint.position;

        if(lineRenderer.enabled == false) return;

        lineRenderer.SetPosition(0, firePoint.position);
        startVFX.transform.position = (Vector2)firePoint.position;
        Debug.DrawLine(direction, firePoint.position, Color.yellow);
        lineRenderer.SetPosition(1, direction.normalized * 20);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)firePoint.position, direction.normalized, direction.magnitude);

        if (hit)
        {
            if (hit.collider.tag == "Enemy" && !hasHit)
            {
                hasHit = true;
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(rangedDamage);
                if(target == null){
                    StopEverything();  
                    return;
                }
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
        hasHit = false;
        laserMaterial.SetFloat("_LaserEdgeThickness", 7.4f);

        StartCoroutine(WaitCooldown());
    }

    IEnumerator WaitCooldown(){
        yield return new WaitForSeconds(1.5f);
        StopEverything();
    }

    public void StopEverything(){
        StopAllCoroutines();
        //stop particle effects
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }

        target = null;
        rangedDamage = Player.Instance.runStats.playerCurrentStats.rangedDamage;
        lineRenderer.enabled = false;

        laserMaterial.SetFloat("_LaserEdgeThickness", 7.6f);
        isShooting = false;

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