using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action UpdateAction;
    private PlayerMove _playerMove;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy;
        collision.gameObject.TryGetComponent<EnemyController>(out enemy);
        float dis = Vector2.Distance(Vector2.zero, enemy.transform.position);
        
        enemy.PushEnemy();

        if (dis >= _playerMove.Radius)
        {
            UIManager.Instance.MessageTextOut("PERFECT", 1f);

            GameManager.Instance.Score += 2;
        }
        else
        {
            GameManager.Instance.Score++;
        }
    }

    void Update()
    {
        UpdateAction.Invoke();
    }
}
