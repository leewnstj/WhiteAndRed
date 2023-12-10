using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer _lineRender;

    private void Awake()
    {
        _lineRender = GetComponent<LineRenderer>();
    }

    public void DrawCircle(int steps, float radius)
    {
        _lineRender.loop = true;
        _lineRender.positionCount = steps;

        float angle = 0f;

        for (int i = 0; i < steps; i++)
        {
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            _lineRender.SetPosition(i, new Vector3(x, y, 0f));

            angle += 2f * Mathf.PI / steps;
        }
    }
}