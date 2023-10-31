using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool PerfectSocre;

    public event Action UpdateAction;
    private PlayerMove _playerMove;
    private ScoreSystem _scoreSystem;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _scoreSystem = GameObject.Find("GameManager").GetComponent<ScoreSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy;
        collision.gameObject.TryGetComponent<EnemyController>(out enemy);

        float dis = Vector2.Distance(Vector2.zero, enemy.transform.position);

        if(dis >= _playerMove.Radius)
        {
            UIManager.Instance.TextOut("PERFECT", 1f);

            _scoreSystem.Score += 2;
        }
        else
        {
            _scoreSystem.Score++;
        }

        enemy.PushEnemy();
    }

    void Update()
    {
        UpdateAction.Invoke();
    }
}
