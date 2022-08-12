using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class ChooseMap : MonoBehaviour
{
    public TMP_InputField speedInputField;
    public Slider speedSlider;
    public TMP_InputField hpInputField;
    public Slider hpSlider;
    public TMP_InputField moneyInputField;
    public Slider moneySlider;
    public int mapNum = 0;
    public Image mapImage;
    public TextMeshProUGUI mapsName;
    public Sprite[] maps;
    public string[] names;
    private string saveFile;
    public TMP_InputField speed;
    public TMP_InputField health;
    public TMP_InputField money;
    public TMP_InputField cash;
    public TMP_InputField lives;
    public Toggle camo;
    public TMP_InputField rounds;
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
        if (rounds.text != "") {saveFile += rounds.text + "}"; } else {  saveFile += "50}"; }
        if (speed.text != "") { saveFile += speed.text + "}"; } else {  saveFile += "100}"; }
        if (health.text != "") { saveFile += health.text + "}"; } else {  saveFile += "100}"; }
        if (money.text != "") { saveFile += money.text + "}"; } else {  saveFile += "100}"; }
        if (cash.text != "") { saveFile += cash.text + "}"; } else {  saveFile += "500}"; }
        if (lives.text != "") { saveFile += lives.text + "}"; } else {  saveFile += "100}"; }
        saveFile += Convert.ToInt32(camo.isOn).ToString();
        StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/" + "saveFile.saveFile");
        streamWriter.Write(saveFile);
        streamWriter.Close();
        SceneManager.LoadScene("Game");
    }
}
