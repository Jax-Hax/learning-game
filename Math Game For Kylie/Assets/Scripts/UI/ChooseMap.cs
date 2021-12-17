using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMap : MonoBehaviour
{
    public void WhatMap(int mapName)
    {
        PlayerPrefs.SetInt("MapName", mapName);
    }
    public void ChooseDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("DifficultyName", difficulty);
        SceneManager.LoadScene("Game");
    }
}
