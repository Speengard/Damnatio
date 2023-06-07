using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPowerUp : MonoBehaviour
{
    private CircleCollider2D cd;
    public GameObject toShow;
    public bool canShow = true;
    private void Awake() {
        cd = GetComponent<CircleCollider2D>();
        canShow = true;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && canShow){
            Time.timeScale = 0;
            toShow.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(waitBeforeOpen());
        }
    }

    IEnumerator waitBeforeOpen(){
        yield return new WaitForSeconds(0.5f);
        canShow = true;
    }

}
