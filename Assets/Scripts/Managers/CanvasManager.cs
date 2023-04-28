using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; } 
    private void Awake()
    {
        if (Instance == null) Instance = this;
		
        if (Instance != this) 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
}
