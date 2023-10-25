using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action UpdateAction;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateAction.Invoke();
    }
}
