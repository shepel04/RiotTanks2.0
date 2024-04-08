using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthProgressBar : MonoBehaviour
{
    public TankController PlayerController;
    public Slider Bar;

    private void Update()
    {
        Bar.value = PlayerController.HealthPoints / 100;
    }
}
