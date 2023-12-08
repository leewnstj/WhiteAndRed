using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingUIManager : MonoBehaviour
{
    public GameObject[] objArr;

    private TextMeshProUGUI[] _nameTex;
    private TextMeshProUGUI[] _scoreTex;

    private void Awake()
    {
        _nameTex = new TextMeshProUGUI[objArr.Length];

        for (int i = 0; i < objArr.Length; i++)
        {
            _nameTex[i] = objArr[i].transform.Find("UserName").GetComponent<TextMeshProUGUI>();
            _scoreTex[i] = objArr[i].transform.Find("Score").GetComponent<TextMeshProUGUI>();
        }
    }

    public void LeaderBoardUI(string userName, int score, int index)
    {
        _nameTex[index].text = userName;
        _scoreTex[index].text = $"{score}";
    }
}
