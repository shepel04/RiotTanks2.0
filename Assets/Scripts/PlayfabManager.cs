using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;
using TMPro;
using LoginResult = PlayFab.ClientModels.LoginResult;
using PlayFabError = PlayFab.PfEditor.EditorModels.PlayFabError;

public class PlayfabManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;

    private void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
    }

    void OnSucces(LoginResult result)
    {
        Debug.Log("Succesfully login/account create!");
    }
    
    void  OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                     StatisticName = "DestroyedEnemies",
                     Value = score
                }
            }
        };
        
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            GameObject newObj = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newObj.GetComponentInChildren<TextMeshProUGUI[]>();
            texts[0].text = item.Position.ToString();
            texts[1].text = item.PlayFabId;
            texts[2].text = item.StatValue.ToString(); 
        }
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }

    
}
