using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPortal : MonoBehaviour
{
    public GameObject portal;

    private void Start() {
        portal = GameObject.Find("Portal");
    }
    private void OnEnable() {
        portal = GameObject.Find("Portal");
    }

    private void Update() {
        if(portal != null){

        transform.rotation = Quaternion.LookRotation(Vector3.forward, portal.transform.position - transform.position);
        }
    }

}
