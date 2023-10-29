using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _coolTime;
    public static SpawnManager Instance;

    private float _currentTime;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("SpawnManager is NULL");
        Instance = this;

        _currentTime = _coolTime;
    }

    public Vector2 SpawnPoint()
    {
        Vector3 value = Random.insideUnitCircle * 7;
        Vector2 point = value.magnitude * value;

        if (point.x >= -24f && point.x <= 24f && point.y <= 13.5f && point.y >= -13.5f)
        {
            
        }

        return point;
    }

    private void Update()
    {
        if(_currentTime < 0)
        {
            _currentTime = _coolTime;
            SpawnEnemy();
        }
        _currentTime -= Time.deltaTime;
    }

    private void SpawnEnemy()
    {
        EnemyController enemy = PoolManager.Instance.Pop("Enemy") as EnemyController;

        enemy.transform.position = SpawnPoint();
    }
}