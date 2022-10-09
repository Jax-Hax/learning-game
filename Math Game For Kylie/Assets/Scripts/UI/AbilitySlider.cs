using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlider : MonoBehaviour
{
    public Image slider;
    private float ability;
    private int index;
    private int maxIndex;
    public void SetCooldown(int abilityCooldown)
    {
        index = 0;
        maxIndex = abilityCooldown;
        slider.fillAmount = 1;
        ability = (float)1 / abilityCooldown;
        InvokeRepeating("ChangeSliderAm", 0f, 1f);
    }
    public IEnumerator ChangeSomeValue(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            slider.fillAmount = Mathf.Lerp(oldValue, newValue, t / duration);
            yield return null;
        }
        slider.fillAmount = newValue;
    }
    void ChangeSliderAm()
    {
        if(index >= maxIndex)
        {
            gameObject.SetActive(false);
            CancelInvoke();
        }
        slider.fillAmount -= ability;
        //ChangeSomeValue(slider.fillAmount, slider.fillAmount - 0.2f, 1f);
        index++;
    }
}
