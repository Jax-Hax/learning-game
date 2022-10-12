using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlider : MonoBehaviour
{
    public Image slider;
    bool isAlreadyGoing;
    public void SetCooldown(int abilityCooldown)
    {
        if (!isAlreadyGoing)
        {
            slider.fillAmount = 1;
            isAlreadyGoing = true;
            StartCoroutine(ChangeSomeValue(1, 0, abilityCooldown));
        }
    }
    public IEnumerator ChangeSomeValue(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            slider.fillAmount = Mathf.Lerp(oldValue, newValue, t / duration);
            yield return null;
        }
        slider.fillAmount = newValue;
        isAlreadyGoing = false;
        gameObject.SetActive(false);
    }
}
