using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] private EnemySpawnSO _so;
    [SerializeField] private float _coolTime;

    private Spawn _spawn;
    private float _currentTime;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("SpawnManager is NULL");
        Instance = this;

        _spawn = GameObject.Find("SpawnPoint").GetComponent<Spawn>();

        _currentTime = _so.CoolTime;
    }

    private void Update()
    {
        if(_currentTime < 0)
        {
            _currentTime = _so.CoolTime;
            SpawnEnemy();
        }
        _currentTime -= Time.deltaTime;
    }

    private void GameSpawnAdd()
    {

    }

    private void SpawnEnemy()
    {
        EnemyController enemy = PoolManager.Instance.Pop("Enemy") as EnemyController;

        enemy.transform.position = _spawn.SpawnPoint();
        enemy.Enemy(_so.Speed);
    }
}