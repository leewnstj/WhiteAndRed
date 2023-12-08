using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingleTon<SpawnManager>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _cool;
    [SerializeField] private float _addSpeedValue;
    [SerializeField] private float _addTimeValue;

    private Spawn _spawn;
    private float _currentTime;

    private void Awake()
    {
        _spawn = GameObject.Find("SpawnPoint").GetComponent<Spawn>();

        _currentTime = _cool;
    }

    private void Update()
    {   
        if (!GameManager.Instance.GameOver)
        {
            if (_currentTime < 0)
            {
                _currentTime = _cool;

                SpawnEnemy();
            }
            _currentTime -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        EnemyController enemy = PoolManager.Instance.Pop("Enemy") as EnemyController;
        enemy.transform.position = _spawn.SpawnPoint();
        enemy.Enemy(_speed);

        _speed += _addSpeedValue;

        if(_cool >= 1f)
        _cool -= _addTimeValue;
    }
}