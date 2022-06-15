using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveCard : MonoBehaviour
{
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
        waveCreator.LoadWave(waveFile, waveNum);
    }
    public void DeleteCard()
    {
        waveCreator.setSave[waveCreator.currentNum - 1].setSave.RemoveAt(waveNum - 1);
        Destroy(gameObject);
    }
}
