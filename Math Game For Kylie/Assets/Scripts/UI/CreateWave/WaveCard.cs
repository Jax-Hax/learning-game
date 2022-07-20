using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveCard : MonoBehaviour
{
    public WaveSave waveSave;
    public Sprite bloonType;
    public string howMany;
    public bool isCamo;
    public GameObject camoText;
    public Image bloonTypeImage;
    public TextMeshProUGUI waveNumText;
    public TextMeshProUGUI amSpawnedText;
    public int waveNum;
    public string waveFile;
    public WaveCreator waveCreator;
    public void UpdateCard()
    {
        bloonTypeImage.sprite = bloonType;
        waveNumText.text = "Wave " + waveNum.ToString();
        if (isCamo)
        {
            camoText.SetActive(true);
        }
        else
        {
            camoText.SetActive(false);
        }
        amSpawnedText.text = howMany + " fruits";
    }
    public void EditCard()
    {
        waveCreator.LoadWave(waveFile, waveNum, gameObject);
    }
    public void DeleteCard()
    {
        waveCreator.setSave[waveCreator.amOfRoundNum].waveSaves.Remove(waveNum);
        Destroy(gameObject);
    }
}
