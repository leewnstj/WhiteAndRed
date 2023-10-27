using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolableMono
{
    [SerializeField] private float _speed;


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, _speed * Time.deltaTime);
    }

    public override void Init()
    {

    }
}
