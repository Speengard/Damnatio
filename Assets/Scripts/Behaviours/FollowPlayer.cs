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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
    }

    void LateUpdate()
    {   
        float cameraOffset = 2;
        float width = 2 * levelManager.roomWidth;
        float height = 2 * levelManager.roomHeight;
        
        // set the camera position as the player position with an offset
        Vector3 cameraPosition = player.position + new Vector3(xOffset, yOffset, zOffset);

        // make the camera stay in bounds
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, (- width - cameraOffset) / 2 + Camera.main.orthographicSize * Camera.main.aspect, (width + cameraOffset) / 2 - Camera.main.orthographicSize * Camera.main.aspect);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, (- height - cameraOffset) / 2 + Camera.main.orthographicSize, (height + cameraOffset) / 2 - Camera.main.orthographicSize);

        // smooth animation for the camera
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref velocity, smoothDelay);
    }

}
