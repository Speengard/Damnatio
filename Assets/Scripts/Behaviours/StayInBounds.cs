using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInBounds : MonoBehaviour
{
    private LevelManager levelManager;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
    }

    void LateUpdate()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        xPos = Mathf.Clamp(xPos, -levelManager.roomWidth, levelManager.roomWidth);
        yPos = Mathf.Clamp(yPos, -levelManager.roomHeight, levelManager.roomHeight);

        transform.position = new Vector2(xPos, yPos);
    }
}
