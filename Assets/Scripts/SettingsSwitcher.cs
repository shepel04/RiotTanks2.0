using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SettingsSwitcher : MonoBehaviour
{
    public GameObject SettingsCanvas;
    public GameObject MenuElementsCanvas;
    
    private bool _isActive;

    public void SwitchSettings()
    {
        if (!_isActive)
        {
            MenuElementsCanvas.SetActive(false);
            SettingsCanvas.SetActive(true);
            _isActive = true;
        }
        else
        {
            SettingsCanvas.SetActive(false);
            MenuElementsCanvas.SetActive(true);
            _isActive = false;
        }
    }
}
