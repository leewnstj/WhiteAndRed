using Firebase.Database;
using Firebase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;

public class RankingDataBase : SingleTon<RankingDataBase>
{
    DatabaseReference databaseReference;

    void Start()
    {
        // Firebase 초기화
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void UpdateLeaderboard(string playerName, int score)
    {
        // 랭킹 정보 업데이트
        string key = databaseReference.Child("leaderboard").Push().Key;
        LeaderboardEntry entry = new LeaderboardEntry(playerName, score);
        string json = JsonUtility.ToJson(entry);
        databaseReference.Child("leaderboard").Child(key).SetRawJsonValueAsync(json);
    }
}

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int score;

    public LeaderboardEntry(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}
