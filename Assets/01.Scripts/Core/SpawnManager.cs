using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("SpawnManager is NULL");
        Instance = this;
    }
}
