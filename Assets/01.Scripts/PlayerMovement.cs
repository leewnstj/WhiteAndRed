using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float radius;
    private InputAgent _input;

    private void Awake()
    {
        _input = GetComponent<InputAgent>();

        _input.PressKey += SetPress;
        _input.ReleaseKey += SetRelease;
    }

    private void SetRelease()
    {
        transform.position = Vector3.zero;
    }

    private void SetPress(Vector2 pos)
    {
        transform.position = pos;
    }

    private void Update()
    {
        transform.position = Vector2.ClampMagnitude((Vector2)transform.position, radius);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Vector2.zero, radius);
    }

#endif
}