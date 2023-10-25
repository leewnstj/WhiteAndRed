using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAgent : MonoBehaviour
{
    public event Action<Vector2> PressKey;
    public event Action ReleaseKey;

    private Camera _cam;
    private Vector2 _mousePos;
    public Vector2 _pos;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        _mousePos = Input.mousePosition;
        _pos = _cam.ScreenToWorldPoint(_mousePos);


        MousePress();
        MouseRelease();
    }

    private void MousePress()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PressKey.Invoke(_pos);
        }
    }

    private void MouseRelease()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseKey?.Invoke();
        }
    }
}
