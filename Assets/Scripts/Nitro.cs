using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Nitro : MonoBehaviour
{
    public float SpeedMultiplier = 2f;
    
    private TankController _player;
    private Slider _nitroSlider;
    private Coroutine _speedBoostCoroutine;
    private float _remainingTime;
    private bool _isBoostActive;

    private void Start()
    {
        GameObject nitroSlObj = GameObject.Find("NitroBar");
        _nitroSlider = nitroSlObj.GetComponent<Slider>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Nitro"))
        {
            _player = GetComponent<TankController>();
            Destroy(other.gameObject);

            if (!_isBoostActive)
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
        _player.MovementSpeed *= SpeedMultiplier;
        _isBoostActive = true;
        _remainingTime = 10f; 
        _nitroSlider.value = 1f;

        while (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;

            if (_nitroSlider != null)
            {
                _nitroSlider.value = _remainingTime / 10f; 
            }

            yield return null;
        }

        _player.MovementSpeed /= SpeedMultiplier;
        _isBoostActive = false;

        Debug.Log("Stop");
    }
}