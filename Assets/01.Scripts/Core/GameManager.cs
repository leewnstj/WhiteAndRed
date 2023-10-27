using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
