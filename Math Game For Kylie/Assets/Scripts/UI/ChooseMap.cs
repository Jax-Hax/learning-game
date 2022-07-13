using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChooseMap : MonoBehaviour
{
    public TMP_InputField speedInputField;
    public Slider speedSlider;
    public TMP_InputField hpInputField;
    public Slider hpSlider;
    public TMP_InputField moneyInputField;
    public Slider moneySlider;
    public void SliderUpdate() { speedInputField.text = speedSlider.value.ToString(); }
    public void InputFieldUpdate() { speedSlider.value = System.Convert.ToSingle(speedInputField.text); }
    public void HealthSliderUpdate() { hpInputField.text = hpSlider.value.ToString(); }
    public void HealthInputFieldUpdate() { hpSlider.value = System.Convert.ToSingle(hpInputField.text); }
    public void MoneySliderUpdate() { moneyInputField.text = moneySlider.value.ToString(); }
    public void MoneyInputFieldUpdate() { moneySlider.value = System.Convert.ToSingle(moneyInputField.text); }
    /*public void WhatMap(int mapName)
    {
        PlayerPrefs.SetInt("MapName", mapName);
    }
    public void ChooseDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("DifficultyName", difficulty);
        SceneManager.LoadScene("Game");
    }
    */
}
