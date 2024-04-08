using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MineSliderAnimator : MonoBehaviour
{ 
    public Slider MineSlider;

    private Coroutine _mineCoroutine;

    public void AnimateMineSlider()
    {
        _mineCoroutine = StartCoroutine(ActivateMine()); 
    }

    IEnumerator ActivateMine()
    {
        yield return AnimateSlider(0f, 1f, 0.4f);

        yield return AnimateSlider(1f, 0f, 0.4f);
    }

    IEnumerator AnimateSlider(float startValue, float endValue, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(timer / duration);

            MineSlider.value = Mathf.Lerp(startValue, endValue, progress);

            yield return null;
        }
    }
}