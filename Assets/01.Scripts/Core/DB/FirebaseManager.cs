using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Threading.Tasks;

public delegate void LoginEvent(bool success, FirebaseUser user);
public delegate void CreateAccountEvent(bool success);

[Serializable]
public class FirebaseUserData
{

    public string userName;
    public int userScore;
    public string lastLoginTime;
    public int LoginCnt;
    public int LoginAmount;

}

public class FirebaseManager : MonoBehaviour
{
    private FirebaseAuth _auth;
    private FirebaseUser _user;
    private DatabaseReference _db;

    public bool IsAuthError { get; private set; }
    public FirebaseUserData UserData { get; private set; }

    public string day { get; private set; }

    public static FirebaseManager Instance;

    public string Errortex { get; set; }

    private void Awake()
    {
        StartAuth();

        DontDestroyOnLoad(this);
    }

    public void StartAuth()
    {

        Instance = this;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {

                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseDatabase.DefaultInstance.RootReference;

            }
            else
            {

                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                IsAuthError = true;

            }

        });
    }

    public async void Login(string email, string password, LoginEvent loginEvent)
    {

        try
        {

            var res = await _auth.SignInWithEmailAndPasswordAsync(email, password);
            _user = res.User;

            loginEvent?.Invoke(true, _user);

        }
        catch (Exception ex)
        {

            Debug.LogError(ex.Message);
            Errortex = ex.Message;
            loginEvent?.Invoke(false, null);

        }

    }

    public async void CreateAccount(string email, string password, string userName, CreateAccountEvent callback, bool login = true)
    {

        try
        {

            var res = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);

            _user = res.User;

            if (login)
            {

                Login(email, password, (success, user) =>
                {

                    if (success)
                    {

                        CreateUserData(userName);

                    }

                    callback?.Invoke(success);

                });

            }
            else
            {

                callback?.Invoke(true);

            }



        }
        catch (Exception ex)
        {


            Debug.LogError(ex.Message);
            Errortex = ex.Message;
            callback?.Invoke(false);

        }

    }

    public void CreateUserData(string userName)
    {

        if (_user == null) return;

        UserData = new FirebaseUserData { userName = userName };

        _db.Child("users").Child(_user.UserId).Child("UserData").SetValueAsync(JsonUtility.ToJson(UserData));

    }

    public IEnumerator ChangePassword(string newPassword)
    {
        var task = _user.UpdatePasswordAsync(newPassword);
        yield return new WaitUntil(() => task.IsCompleted);
        if(task.Exception == null)
        {
            print("changed password");
        }
    }

    public void UserScore(int score)
    {
        if (_user == null)
        {
            Debug.Log("NULL");
            return;
        }
        UserData.userScore = score;

        SaveUserData();

    }

    public void LastLoginTime(string time)
    {
        if (_user == null)
        {
            Debug.Log("NULL");
            return;
        }
        UserData.lastLoginTime = time;

        SaveUserData();

    }
    
    public void LoginCheck()
    {
        if (_user == null)
        {
            Debug.Log("NULL");
            return;
        }
        if ((DateTime.Now - DateTime.Parse(FirebaseManager.Instance.UserData.lastLoginTime)).TotalSeconds > 1)
        {
            UserData.LoginAmount++;
            if ((DateTime.Now - DateTime.Parse(FirebaseManager.Instance.UserData.lastLoginTime)).TotalMinutes > 1)
            {
                UserData.LoginCnt--;
                if(UserData.LoginCnt < 1)
                    UserData.LoginCnt = 1;
                Debug.Log("Successasdf");
            }
            else
            {
                UserData.LoginCnt++;
                Debug.Log("Success");
            }
            SaveUserData();
        }
    }

    public async Task LoadUserdata()
    {

        if (_user == null) return;

        var res = await _db.Child("users").Child(_user.UserId).Child("UserData").GetValueAsync();

        if (res != null && res.Value != null)
        {

            UserData = JsonUtility.FromJson<FirebaseUserData>(res.Value.ToString());

        }

    }

    public async Task<List<FirebaseUserData>> LoadAllUserData()
    {
        List<FirebaseUserData> list = new List<FirebaseUserData>();
        var res = await _db.Child("users").GetValueAsync();

        foreach (var data in res.Children)
        {
            list.Add(JsonUtility.FromJson<FirebaseUserData>(data.Child("UserData").Value.ToString()));            
        }
        return list;
    }

    public void SaveUserData()
    {

        if (_user == null) return;

        _db.Child("users").Child(_user.UserId).Child("UserData").SetValueAsync(JsonUtility.ToJson(UserData));

    }

    public async Task DayCheck()
    {
        var res = await _db.Child("Christmas").GetValueAsync();;
        day = res.Value.ToString();
    }

}