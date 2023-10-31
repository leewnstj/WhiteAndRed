using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Score = 0;
    public int BestScore = 0;

    [SerializeField] private PoolingableSO _poolingList;

    private void Awake()
    {
        if (Instance != null) Debug.LogError($"GameManager is NULL");

        MakePool();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingList.pools.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.count));
    }
}
