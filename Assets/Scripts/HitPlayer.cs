 using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class HitPlayer : MonoBehaviour
{
    private Slider _healthBar;
    private TankController _playerController;

    private void Start()
    {
       GameObject hbObj = GameObject.Find("Health");

       _healthBar = hbObj.GetComponent<Slider>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerController = other.gameObject.GetComponent<TankController>();
            if (_playerController.IsArmorActive)
            {
                _playerController.HealthPoints -= 6f;
            }
            else
            {
                _playerController.HealthPoints -= 12.5f;
            }
            _healthBar.value = _playerController.HealthPoints / 100f;
            Debug.Log(_playerController.HealthPoints);
        }
    }
}
