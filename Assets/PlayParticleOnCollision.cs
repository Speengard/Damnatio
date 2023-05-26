using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayParticleOnCollision : MonoBehaviour{
    public ParticleSystem ps;
    private void Awake() {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("triggered");
        StartCoroutine(playParticles(() =>{
            ps.Stop();
        }));
    }

    IEnumerator playParticles(Action callback = null){
        ps.Play();
        yield return new WaitForSeconds(1f);
        callback?.Invoke();
    }
    
}
