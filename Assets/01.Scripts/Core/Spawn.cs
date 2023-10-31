using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> list = new();

    public Vector2 point;

    public Vector2 SpawnRandom()
    {
        int random = Random.Range(0, 4);

        return list[random].transform.position;
    }
    
    public Vector2 SpawnPoint()
    {
        Vector3 value = Random.insideUnitCircle * 7;
        point = value.magnitude * value;

        if (point.x >= -24f && point.x <= 24f && point.y <= 13.5f && point.y >= -13.5f)
        {
            point = SpawnRandom();
        }

        return point;
    }
}
