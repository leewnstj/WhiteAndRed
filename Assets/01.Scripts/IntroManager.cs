using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    private bool _isLogin;
    private bool _isSignUP;

    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _nameObj;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private TMP_InputField _inputField_email;
    [SerializeField] private TMP_InputField _inputField_password;
    [SerializeField] private TMP_InputField _inputField_user;

    private void Awake()
    {
        _panel.SetActive(false);
        _nameObj.SetActive(false);
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

    private void HandleLoginEnd(bool success, FirebaseUser user)
    {
        if(!success)
        {
            _errorText.text = FirebaseManager.Instance.Errortex;
        }
        else
        {
            Debug.Log("æ¿ ¿Ãµø");
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
            Debug.Log("æ¿ ¿Ãµø");
        }
    }
}