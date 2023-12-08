using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    [SerializeField] private PoolingableSO _poolingList;

    public int _curScore = 0;
    public int Score
    {
        get
        {
            return _curScore;
        }
        set
        {
            _curScore = value;
            UIManager.Instance.ScoreTextOut(_curScore);
        }
    }
    public int BestScore;

    private bool _gameOver;
    public event Action OnGameOverEvent;
    public bool GameOver
    {
        get
        {
            return _gameOver;
        }
        set
        {
            _gameOver = value;
            OnGameOverEvent?.Invoke();
        }
    }


    private void Awake()
    {
        MakePool();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingList.pools.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.count));
    }
}
