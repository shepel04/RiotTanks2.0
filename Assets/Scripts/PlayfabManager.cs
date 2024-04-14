using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DefaultNamespace;
using Photon.Pun.Demo.PunBasics;
using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    public GameObject RowPrefab;
    public Transform RowsParent;
    public GameObject NameWindow;
    public GameObject MenuObjects;
    public LeaderboardSwitcher Switcher;
    public TMP_InputField NameInput;
    public TextMeshProUGUI PlayerName;
    
    private void Start()
    {
        Login();
    }

    void Login()
    {
       

        var request = new LoginWithCustomIDRequest
        {
            //CustomId = SystemInfo.deviceUniqueIdentifier,
            CustomId = SystemInfo.deviceModel,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }
    
    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successfully logged in!");
        SendCurrentHighscore();
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (name == null)
        {
            NameWindow.SetActive(true);
            MenuObjects.SetActive(false);
        }
        else
        {
            NameWindow.SetActive(false);
            //Switcher.SwitchLeaderboard();
        }
        PlayerName.text = result.InfoResultPayload.PlayerProfile.DisplayName;
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = NameInput.text
            
        };
        
        PlayerPrefs.SetString("PlayerName", NameInput.text);
        PlayerPrefs.Save();
        
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
        
        Switcher.SwitchLeaderboard();
        NameWindow.SetActive(false);
    }

    void OnSucces(LoginResult result)
    {
        Debug.Log("Succesfully login/account create!");
    }
    
    void OnError(PlayFabError error)
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
        Debug.Log("Data sent successfull!");
    }

    public void SendCurrentHighscore()
    {
        SendLeaderboard(PlayerPrefs.GetInt("MaxDestroyedTanks", 0));
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "DestroyedEnemies",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in RowsParent)
        {
            Destroy(item.gameObject);
        }
        

        foreach (var item in result.Leaderboard)
        { 
            var request = new GetAccountInfoRequest
            {
                PlayFabId = item.PlayFabId
            };
            
            Debug.Log(item.Position.ToString() + " " + item.PlayFabId + " " + item.StatValue.ToString());
            GameObject newObj = Instantiate(RowPrefab, RowsParent);
            TextMeshProUGUI[] texts = newObj.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            
            
        }
    }
    
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }

    
}
