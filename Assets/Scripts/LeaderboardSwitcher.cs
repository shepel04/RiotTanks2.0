using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LeaderboardSwitcher : MonoBehaviour
{
    public GameObject LeaderboardCanvas;
    public GameObject MenuElementsCanvas;
    public PlayfabManager Manager;
    
    private bool _isLeaderboardActive;

    public void SwitchLeaderboard()
    {
        if (!_isLeaderboardActive)
        {
            MenuElementsCanvas.SetActive(false);
            LeaderboardCanvas.SetActive(true);
            Manager.GetLeaderboard();
            _isLeaderboardActive = true;
        }
        else
        {
            LeaderboardCanvas.SetActive(false);
            MenuElementsCanvas.SetActive(true);
            _isLeaderboardActive = false;
        }
    }
}
