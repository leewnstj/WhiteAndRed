using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PoolableMono
{
    [SerializeField] private float _speed;


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, _speed * Time.deltaTime);

        if(transform.position == Vector3.zero )
        {
            PoolManager.Instance.Push(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("sdf");
            PoolManager.Instance.Push(this);
        }
    }

    public override void Init()
    {

    }
}
