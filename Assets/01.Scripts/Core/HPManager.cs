using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPManager : SingleTon<HPManager>
{
    [SerializeField] private GameObject[] _hp;
    [SerializeField] private GameObject _hpGroup;
    
    private Vector2 _hpFirstOffset;

    public int HP { get; set; }

    private void Awake()
    {
        HP = _hp.Length;
        _hpFirstOffset = _hpGroup.transform.position;

        _hpGroup.transform.DOMove(new Vector2(_hpFirstOffset.x, _hpFirstOffset.y - 110f), 3f);
    }

    private void Start()
    {
        for(int i = 0; i < _hp.Length; i++)
        {
            _hp[i].SetActive(true);
        }
    }

    public void DestroyHP(int value)
    {
        HP--;
        _hp[value].SetActive(false);
        if(HP == 0 )
        {
            GameManager.Instance.GameOver = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        _hpGroup.transform.DOMove(_hpFirstOffset, 5f);
    }
}
