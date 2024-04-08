using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairTank : MonoBehaviour
{
    private TankController _playerController;
    private Slider _healthBar;
    private RepairKitSliderAnimator _repAnim;
    private void Start()
    {
        _playerController = GetComponent<TankController>();
        
        GameObject hbObj = GameObject.Find("Health");
        _healthBar = hbObj.GetComponent<Slider>();

        _repAnim = GetComponent<RepairKitSliderAnimator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RepairKit"))
        {
            _repAnim.AnimateRepairSlider();
            Destroy(other.gameObject);
            _playerController.HealthPoints = 100;
            _healthBar.value = 1;
        }
    }
}
