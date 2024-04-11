using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public GameObject PauseCanvas;
    public GameObject HUDCanvas;
    
    private bool _isPauseActive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseSwitcher();
        }
    }

    public void PauseSwitcher()
    {
        if (!_isPauseActive)
        {
            PauseCanvas.SetActive(true);
            HUDCanvas.SetActive(false);
            _isPauseActive = true;
            Time.timeScale = 0;
        }
        else
        {
            PauseCanvas.SetActive(false);
            HUDCanvas.SetActive(true);
            _isPauseActive = false;
            Time.timeScale = 1;
        }
    }
}
