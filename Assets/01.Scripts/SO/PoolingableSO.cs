using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolSO
{
    public PoolableMono prefab;
    public int count;
}

[CreateAssetMenu(menuName = "SO/Pooling")]
public class PoolingableSO : ScriptableObject
{
    public List<PoolSO> pools;
}
