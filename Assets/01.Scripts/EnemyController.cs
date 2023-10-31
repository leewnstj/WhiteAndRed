using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PoolableMono
{
    private float _speed;

    public void Enemy(float value)
    {
        _speed = value;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, _speed * Time.deltaTime);

        if(transform.position == Vector3.zero )
        {
            PushEnemy();
        }
    }

    public void PushEnemy()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {

    }
}
