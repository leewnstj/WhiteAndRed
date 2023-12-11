using DG.Tweening;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroUIManager : MonoBehaviour
{
    #region Property
    private bool _isLogin;
    private bool _isSignUP;

    [Header("Title")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _titleRedText;
    [Header ("Login Panel")]
    [SerializeField] private GameObject _panel;
    [Header ("Sign TextArea")]
    [SerializeField] private GameObject _nameObj;
    [Header("LoginBtn In Login Panel")]
    [SerializeField] private TextMeshProUGUI _text;
    [Header("Text In Login Panel")]
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private TMP_InputField _inputField_email;
    [SerializeField] private TMP_InputField _inputField_password;
    [SerializeField] private TMP_InputField _inputField_user;

    private TextMeshProUGUI _loginBtn;
    private TextMeshProUGUI _signBtn;

    private DG.Tweening.Sequence _seq;
    #endregion

    private void Awake()
    {
        _seq = DOTween.Sequence();

        _loginBtn = transform.Find("Login").GetComponentInChildren<TextMeshProUGUI>();
        _signBtn = transform.Find("SignUP").GetComponentInChildren<TextMeshProUGUI>();
        _panel.SetActive(false);
        _nameObj.SetActive(false);
    }

    private void Start()
    {
        _seq.Append(_titleText.DOFade(1, 3f))
            .Join(_titleRedText.DOFade(1,3f))
            .Append(_loginBtn.DOFade(1, 1f))
            .Join(_signBtn.DOFade(1, 1f));
    }

    public void BtnLogin()
    {
        _isLogin = true;
        ShowPannel();
    }

    public void BtnSignUp()
    {
        _isSignUP = true;
        ShowPannel();
    }

    public void X()
    {
        _isLogin = false;
        _isSignUP = false;
        _panel.SetActive(_isLogin || _isSignUP);
        _nameObj.SetActive(_isSignUP);

        _inputField_email.text = null;
        _inputField_password.text = null;
        _inputField_user.text = null;
        _errorText.text = null;
    }

    private void ShowPannel()
    {
        _panel.SetActive(_isLogin || _isSignUP);
        _nameObj.SetActive(_isSignUP);

        if (_isLogin) _text.text = "LOGIN";
        else if (_isSignUP) _text.text = "SIGN UP";
    }

    public void DaeWonBtn()
    {
        if(_isLogin)
        {
            FirebaseManager.Instance.Login(_inputField_email.text, _inputField_password.text, HandleLoginEnd);
        }
        else if (_isSignUP)
        {
            FirebaseManager.Instance.CreateAccount(
                _inputField_email.text, _inputField_password.text, _inputField_user.text, HandleLoginEnd);
        }
    }

    private async void HandleLoginEnd(bool success, FirebaseUser user)
    {
        if(!success)
        {
            _errorText.text = FirebaseManager.Instance.Errortex;
        }
        else
        {

            await FirebaseManager.Instance.LoadUserdata();
            FirebaseManager.Instance.LastLoginTime(DateTime.Now.ToString("t"));
            SceneMove();
        }
    }
    private void HandleLoginEnd(bool success)
    {
        if(!success)
        {
            _errorText.text = FirebaseManager.Instance.Errortex;
        }
        else
        {
            FirebaseManager.Instance.LastLoginTime(DateTime.Now.ToString("t"));
            SceneMove();
        }
    }

    private void SceneMove()
    {
        FirebaseManager.Instance.LoginCheck();
        _isLogin = false;
        _isSignUP = false;
        _panel.SetActive(_isLogin || _isSignUP);
        _nameObj.SetActive(_isSignUP);

        _seq.Append(_titleText.DOFade(0, 1))
            .Join(_titleRedText.DOFade(0, 1f))
            .Join(_loginBtn.DOFade(0, 1))
            .Join(_signBtn.DOFade(0, 1));

        SceneManager.LoadScene(SceneList.Main);
    }
}