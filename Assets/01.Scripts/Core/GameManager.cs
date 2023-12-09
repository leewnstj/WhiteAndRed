using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingleTon<GameManager>
{
    #region Data
    [SerializeField] private PoolingableSO _poolingList;

    //인게임 스코어
    private int _curScore = 0;
    public int Score
    {
        get{ return _curScore; }
        set
        {
            _curScore = value;
            UIManager.Instance.ScoreTextOut(_curScore);
        }
    }

    //베스트 스코어
    private int _curBest;
    public int BestScore
    {
        get { return _curBest; }
        set
        {
            _curBest = value;
            FirebaseManager.Instance.UserScore(_curBest);
        }
    }

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
    #endregion

    private void Awake()
    {
        MakePool();
        OnGameOverEvent += ScoreReset;
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingList.pools.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.count));
    }

    private void ScoreReset()
    {
        if(_gameOver)
        {
            BestScore = _curScore;
            _curScore = 0;
            Debug.Log($"{BestScore} , {_curScore}");
            SceneManager.LoadScene(SceneList.Main);
        }
    }
}
