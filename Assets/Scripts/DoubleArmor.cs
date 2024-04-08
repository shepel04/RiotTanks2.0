using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoubleArmor : MonoBehaviour
{
    private TankController _player;
    private Slider _doubleArmroSlider;
    private Coroutine _speedBoostCoroutine;
    private float _remainingTime;
    private bool _isBoostActive;

    private void Start()
    {
        GameObject dArmor = GameObject.Find("DoubleArmorBar");
        _doubleArmroSlider = dArmor.GetComponent<Slider>();
        _player = GetComponent<TankController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DoubleArmor"))
        {
            Destroy(other.gameObject);

            if (!_player.IsArmorActive)
            {
                if (_player != null)
                {
                    if (_speedBoostCoroutine != null)
                    {
                        StopCoroutine(_speedBoostCoroutine);
                    }

                    _speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine());
                }
            }
            else
            {
                _remainingTime = 10f;
            }
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        _player.IsArmorActive = true;
        _remainingTime = 10f; 
        _doubleArmroSlider.value = 1f;
        while (_remainingTime > 0)
        {

            _remainingTime -= Time.deltaTime;

            if (_doubleArmroSlider != null)
            {
                _doubleArmroSlider.value = _remainingTime / 10f; 
            }

            yield return null;
        }
        
        _player.IsArmorActive = false;
        
        Debug.Log("Stop");
    }
}