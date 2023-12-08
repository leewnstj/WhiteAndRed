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

}

public class FirebaseManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private FirebaseUser user;
    private DatabaseReference db;

    public bool IsAuthError { get; private set; }
    public FirebaseUserData userData { get; private set; }

    public static FirebaseManager Instance;

    public string Errortex;

    private void Awake()
    {
        StartAuth();
    }

    public void StartAuth()
    {

        Instance = this;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {

                auth = FirebaseAuth.DefaultInstance;
                db = FirebaseDatabase.DefaultInstance.RootReference;

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

            var res = await auth.SignInWithEmailAndPasswordAsync(email, password);
            user = res.User;

            loginEvent?.Invoke(true, user);

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

            var res = await auth.CreateUserWithEmailAndPasswordAsync(email, password);

            user = res.User;

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


        if (user == null) return;

        userData = new FirebaseUserData { userName = userName };

        db.Child("users").Child(user.UserId).Child("UserData").SetValueAsync(JsonUtility.ToJson(userData));

    }

    public async Task LoadUserdata()
    {

        if (user == null) return;

        var res = await db.Child("users").Child(user.UserId).Child("UserData").GetValueAsync();

        if (res != null && res.Value != null)
        {

            userData = JsonUtility.FromJson<FirebaseUserData>(res.Value.ToString());

        }

    }

    public void SaveUserData()
    {

        if (user == null) return;

        db.Child("users").Child(user.UserId).Child("UserData").SetValueAsync(JsonUtility.ToJson(userData));

    }
}
