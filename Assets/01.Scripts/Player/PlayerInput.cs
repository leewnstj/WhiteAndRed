using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> PressKeyAction;
    public event Action ReleaseKeyAction;

    private Camera _cam;
    private PlayerController _controller;

    private Vector2 _mousePos;
    private Vector2 _pos;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _cam = Camera.main;

        _controller.UpdateAction += PressKey;
        _controller.UpdateAction += ReleaseKey;
    }

    public void PressKey()
    {
        _mousePos = Input.mousePosition;

        if (Input.GetMouseButton(0) && !GameManager.Instance.GameOver)
        {
            _pos = _cam.ScreenToWorldPoint(_mousePos);
            PressKeyAction?.Invoke(_pos);
        }
    }

    public void ReleaseKey()
    {
        if(Input.GetMouseButtonUp(0))
        {
            ReleaseKeyAction?.Invoke();
        }
    }
}
