using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float delay;

    public abstract void Attack();
    
    protected PlayerAttackController attackController;
    private void Awake()
    {
        attackController = PlayerAttackController.Instance;
    }
}
