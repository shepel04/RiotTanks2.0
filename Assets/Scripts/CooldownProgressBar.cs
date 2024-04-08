using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CooldownProgressBar : MonoBehaviour
{
    public float СooldownTime = 3f; 
    private float _cooldownTimer = 0f; 

    private Slider _progressBar; 

    private void Start()
    {
        _progressBar = GetComponent<Slider>(); 
        _progressBar.value = 1f; 
    }

    private void Update()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;

            float progress = Mathf.Clamp01(1 - (_cooldownTimer / СooldownTime));
            _progressBar.value = progress;
        }
        else
        {
            _progressBar.value = 1f; 
        }
    }

    public void StartCooldown()
    {
        _cooldownTimer = СooldownTime;
    }
}