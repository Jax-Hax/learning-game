using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class ChooseMap : MonoBehaviour
{
    public TMP_InputField speedInputField;
    public Slider speedSlider;
    public TMP_InputField hpInputField;
    public Slider hpSlider;
    public TMP_InputField moneyInputField;
    public Slider moneySlider;
    public int mapNum;
    public Image mapImage;
    public TextMeshProUGUI mapsName;
    public Sprite[] maps;
    public string[] names;
    private string saveFile;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI health;
    public TextMeshProUGUI money;
    public TextMeshProUGUI cash;
    public TextMeshProUGUI lives;
    public Toggle camo;
    public TextMeshProUGUI rounds;
    public void SliderUpdate() { speedInputField.text = speedSlider.value.ToString(); }
    public void InputFieldUpdate() { speedSlider.value = System.Convert.ToSingle(speedInputField.text); }
    public void HealthSliderUpdate() { hpInputField.text = hpSlider.value.ToString(); }
    public void HealthInputFieldUpdate() { hpSlider.value = System.Convert.ToSingle(hpInputField.text); }
    public void MoneySliderUpdate() { moneyInputField.text = moneySlider.value.ToString(); }
    public void MoneyInputFieldUpdate() { moneySlider.value = System.Convert.ToSingle(moneyInputField.text); }
    public void WhatMap(int mapName)
    {
        mapNum = mapName;
        mapImage.sprite = maps[mapName];
        mapsName.text = names[mapName];
    }
    public void SaveText()
    {
        saveFile = "";
        PlayerPrefs.SetInt("MapName", mapNum);
        saveFile += rounds.text + "}";
        saveFile += speed.text + "}";
        saveFile += health.text + "}";
        saveFile += money.text + "}";
        saveFile += cash.text + "}";
        saveFile += lives.text + "}";
        saveFile += Convert.ToInt32(camo.isOn).ToString();
        StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/" + "saveFile.saveFile");
        streamWriter.Write(saveFile);
        streamWriter.Close();
        SceneManager.LoadScene("Game");
    }
}
