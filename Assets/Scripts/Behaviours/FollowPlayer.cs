using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LevelManager levelManager;
    private float zOffset = -10;
    private float yOffset = 0;
    private float xOffset = 0;
    private float smoothDelay = 0.3f;
    private Vector3 velocity = Vector3.zero;

    // Camera Information
    private Vector3 orignalCameraPos;

    // Shake Parameters
    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.1f;

    private bool canShake = false;
    private float _shakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        orignalCameraPos = player.localPosition;
    }
    private void Update() {
        if (canShake)
        {
            StartCameraShakeEffect();
        }

        
    }
    void LateUpdate()
    {   
        float cameraOffset = 2;

        // set the camera position as the player position with an offset
        Vector3 cameraPosition = player.position + new Vector3(xOffset, yOffset, zOffset);

        // make the camera stay in bounds
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -levelManager.roomWidth + Camera.main.orthographicSize * Camera.main.aspect + cameraOffset, levelManager.roomWidth - Camera.main.orthographicSize * Camera.main.aspect - cameraOffset);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, -levelManager.roomHeight + Camera.main.orthographicSize + cameraOffset, levelManager.roomHeight - Camera.main.orthographicSize - cameraOffset);

        // smooth animation for the camera
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref velocity, smoothDelay);
    }

    public void ShakeCamera()
    {
        canShake = true;
        _shakeTimer = shakeDuration;
        
    }

    public void StartCameraShakeEffect()
    {
        if (_shakeTimer > 0)
        {
            transform.position = transform.position + Random.insideUnitSphere * shakeAmount;
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeTimer = 0f;
            canShake = false;
        }
    }

}
