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
        ability = (float)abilityCooldown / 60;
        slider.fillAmount = 1;
        Debug.Log(ability);
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
