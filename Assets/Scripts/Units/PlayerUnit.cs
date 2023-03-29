using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public int damage;
    public int currentHP;
    public int maxHP;
    public int protect;
    public int currentDev;
    public int maxDev;
    public List<GameObject> wordList = new List<GameObject>();
    public int healingValue = 30;


    public bool takeDamage(int damage)
    {
        if (damage > protect)
        {
            currentHP -= (damage - protect);
        }
        else
        {
            protect -= damage;
        }

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void die()
    {
        
    }

    public void resetValues()
    {
        damage = 0;
    }
    
    
}
