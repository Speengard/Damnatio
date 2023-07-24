using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuitMenu : MonoBehaviour
{
    public TMP_Text animaeText;
    public TMP_Text enemiesText;

    private void OnEnable() {
        animaeText.text = "Animae: " + Player.Instance.collectedSouls;
        enemiesText.text = "Enemies slain " + GameManager.Instance.enemySlain;
    }
}
