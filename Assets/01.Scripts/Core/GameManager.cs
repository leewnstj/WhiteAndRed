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
    private DatabaseReference databaseReference;
    private FirebaseUser user;

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
            SaveBestScore(_curBest);
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
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            if (app == null)
            {
                Debug.LogError("Firebase initialization failed.");
            }
            else
            {
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase initialized successfully.");
            }
        });

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

    private void SaveBestScore(int newBestScore)
    {
        if (databaseReference != null)
        {
            // Firebase에 BestScore 저장
            databaseReference.Child("users").Child(user.UserId).Child("BestScore").SetValueAsync(newBestScore)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        // 저장 실패 처리
                        Debug.LogError("Error saving BestScore to Firebase: " + task.Exception);
                    }
                    else if (task.IsCompleted)
                    {
                        // 저장 성공 처리
                        Debug.Log("BestScore saved to Firebase successfully.");
                    }
                });
        }
        else
        {
            Debug.Log("DB is NULL");
        }
    }
}
