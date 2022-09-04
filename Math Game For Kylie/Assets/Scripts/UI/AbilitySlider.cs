using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlider : MonoBehaviour
{
    public Image slider;
    private float ability;
    public void SetCooldown(int abilityCooldown)
    {
        slider.fillAmount = 1;
        StartCoroutine(AnimateSliderOverTime(abilityCooldown));
    }
    IEnumerator AnimateSliderOverTime(float seconds)
    {
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            slider.fillAmount = Mathf.Lerp(seconds, 0f, lerpValue);
            yield return null;
        }
    }

    public void Update()
    {
        slider.fillAmount = Mathf.MoveTowards(slider.fillAmount, 0, ability * Time.deltaTime);
        if(slider.fillAmount <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
