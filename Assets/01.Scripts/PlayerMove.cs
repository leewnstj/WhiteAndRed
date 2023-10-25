using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _radius;

    private PlayerInput _input;
    private PlayerController _controller;
    private DrawLine _line;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _line = transform.Find("LineRenderer").GetComponent<DrawLine>();
        _controller = GetComponent<PlayerController>();

        _controller.UpdateAction += DrawUpdate;
        _input.PressKeyAction += PressKey;
        _input.ReleaseKeyAction += ReleaseKey;
    }

    private void DrawUpdate()
    {
        _line.DrawCircle(300, _radius);
    }

    public void PressKey(Vector2 pos)
    {
        transform.position = pos;
        
        transform.position = Vector2.ClampMagnitude((Vector2)transform.position, _radius);
    }

    public void ReleaseKey()
    {
        transform.DOMove(Vector2.zero, 0.1f);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Vector2.zero, _radius);
    }

#endif
}
