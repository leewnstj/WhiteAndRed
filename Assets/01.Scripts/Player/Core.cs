using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private float _shake;
    [SerializeField] private float _duration;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraManager.Instance.ShakeCamera(_shake, _duration);
        collision.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy);
        enemy.PushEnemy();
        HPManager.Instance.DestroyHP(HPManager.Instance.HP - 1);
    }
}