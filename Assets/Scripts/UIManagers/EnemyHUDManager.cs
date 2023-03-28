using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHUDManager : MonoBehaviour
{
    public EnemyUnit unit;
    public void updateText()
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text.text = unit.currentHP + "/" + unit.maxHP;
    }
}
