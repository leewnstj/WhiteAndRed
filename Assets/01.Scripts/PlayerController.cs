using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action UpdateAction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy;
        collision.gameObject.TryGetComponent<EnemyController>(out enemy);

        enemy.PushEnemy();
    }

    void Update()
    {
        UpdateAction.Invoke();
    }
}
