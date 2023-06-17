using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPowerUp : MonoBehaviour
{
    public bool canShow = true;
    private void Awake() {
        canShow = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && canShow){
            Time.timeScale = 0;
            CanvasManager.Instance.showPowerUp();
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(waitBeforeOpen());
        }
    }

    IEnumerator waitBeforeOpen(){
        yield return new WaitForSeconds(1f);
        canShow = true;
    }

}
