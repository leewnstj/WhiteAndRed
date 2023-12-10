using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _leaderBoardPanel;

    [SerializeField] private GameObject[] _objArr;

    [SerializeField] private GameObject _dayEventObj;

    private TextMeshProUGUI[] _nameTex;
    private TextMeshProUGUI[] _scoreTex;

    private FirebaseManager _firebaseManager;

    private void Awake()
    {
        #region Firebase
        _firebaseManager = FirebaseManager.Instance;

        if (_firebaseManager != null)
        {
            LoadAndDisplayAllUserData();
        }
        else
        {
            Debug.LogError("FirebaseManager not found in the scene.");
        }
        #endregion
        #region RankArr
        _nameTex = new TextMeshProUGUI[_objArr.Length];
        _scoreTex = new TextMeshProUGUI[_objArr.Length];

        for (int i = 0; i < _objArr.Length; i++)
        {
            _nameTex[i] = _objArr[i].transform.Find("UserName").GetComponent<TextMeshProUGUI>();
            _scoreTex[i] = _objArr[i].transform.Find("Score").GetComponent<TextMeshProUGUI>();
        }
        #endregion

        DayCheck();
    }

    private void LeaderBoardUI(string userName, int score, int index)
    {
        _nameTex[index].text = userName;
        _scoreTex[index].text = $"{score}";
    }

    private async void LoadAndDisplayAllUserData()
    {
        var res = await _firebaseManager.LoadAllUserData();
        res.OrderBy(x => x.userScore);
        int i = 0;

        foreach(var data in res)
        {
            LeaderBoardUI(data.userName, data.userScore, i);
            i++;
        }
    }

    private async void DayCheck()
    {
        await _firebaseManager.DayCheck();

        if(DateTime.Now.ToString("MM-dd") == _firebaseManager.day)
        {
            _dayEventObj.SetActive(true);
        }
    }

    #region Button Event
    public void BtnOn()
    {
        _leaderBoardPanel.SetActive(true);
    }

    public void BtnOff()
    {
        _leaderBoardPanel.SetActive(false);
    }

    public void StartBtn()
    {
        SceneManager.LoadScene(SceneList.Main);
    }
    #endregion
}