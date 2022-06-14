using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System;
public class WaveCreator : MonoBehaviour
{
    public GameObject SetViewer;
    public GameObject Modebuilder;
    public GameObject RoundBuilder;
    public GameObject WaveBuilder;
    private string saveableString;
    private string loadableString;
    private string[] setInfo;
    public GameObject LobbyObject;
    private GameObject LobbyObjectPri;
    private ModeCard modeCard;
    public GameObject RoundObject;
    private GameObject RoundObjectPri;
    private RoundCard roundCard;
    public GameObject WaveObject;
    private GameObject WaveObjectPri;
    private WaveCard waveCard;
    private int setInfoIndex = 0;
    private int setInfoIndex2 = 0;
    private string[] setInfo2;
    public Transform listPosForSet;
    public Transform listPosForRound;
    public Transform listPosForWave;
    private WaveCreator waveCreate;
    public TMP_InputField titleText;
    public TMP_InputField timeBtwWavesText;
    public TMP_InputField amToRepeatText;
    public Sprite[] bloonTypes;
    public TextMeshProUGUI waveTitle;
    public Toggle toggleCamo;
    public TMP_InputField timeBtwBloonsTextWave;
    public TMP_InputField numOfBloons;
    private void Start()
    {
        LoadSetLobby();
        waveCreate = gameObject.GetComponent<WaveCreator>();
    }
    public void LoadWave(string waveFile)
    {
        RoundBuilder.SetActive(false);
        WaveBuilder.SetActive(true);
        setInfo = waveFile.Split(char.Parse(","));
        setInfoIndex = 0;
        foreach (string currentInfo in setInfo)
        {
            if (setInfoIndex == 0)
            {
                waveCard.bloonType = bloonTypes[int.Parse(currentInfo)];
            }
            else if (setInfoIndex == 1)
            {
                timeBtwBloonsTextWave.text = currentInfo;
            }
            else if (setInfoIndex == 2)
            {
                toggleCamo.isOn = Convert.ToBoolean(currentInfo);
            }
            else if (setInfoIndex2 == 3)
            {
                numOfBloons.text = currentInfo;
            }
            setInfoIndex2++;
        }
        setInfoIndex2 = 0;
    }
    public void CreateWave()
    {
        RoundBuilder.SetActive(false);
        WaveBuilder.SetActive(true);
    }
    public void DecodeRound(string loadedString, int roundNum)
    {
        Modebuilder.SetActive(false);
        RoundBuilder.SetActive(true);
        waveTitle.text = "Round " + roundNum;
        setInfo = loadedString.Split(char.Parse(":"));
        setInfoIndex = 0;
        foreach (string currentInfo in setInfo)
        {
            if (setInfoIndex == 0)
            {
                timeBtwWavesText.text = currentInfo;
                setInfoIndex++;
            }
            else if (setInfoIndex2 == 1)
            {
                amToRepeatText.text = currentInfo;
                setInfoIndex++;
            }
            else
            {
                WaveObjectPri = Instantiate(WaveObject, listPosForWave);
                waveCard = WaveObjectPri.GetComponent<WaveCard>();
                waveCard.waveFile = currentInfo;
                setInfo2 = currentInfo.Split(char.Parse(","));
                setInfoIndex2 = 0;
                foreach (string currentInfo2 in setInfo2)
                {
                    if(setInfoIndex2 == 0)
                    {
                        waveCard.bloonType = bloonTypes[int.Parse(currentInfo2)];
                    }
                    else if (setInfoIndex2 == 2)
                    {
                        waveCard.isCamo = Convert.ToBoolean(currentInfo2);
                        waveCard.waveNum = setInfoIndex - 1;
                    }
                    else if(setInfoIndex2 == 3)
                    {
                        waveCard.howMany = currentInfo2;
                        waveCard.waveCreator = waveCreate;
                        waveCard.UpdateCard();
                    }
                    setInfoIndex2++;
                }
                setInfoIndex2 = 0;
            }
        }
        setInfoIndex = 0;
    }
    public void CreateNewRound()
    {
        Modebuilder.SetActive(false);
        RoundBuilder.SetActive(true);
    }
    public void DecodeSet(string loadedString)
    {
        SetViewer.SetActive(false);
        Modebuilder.SetActive(true);
        setInfo = loadedString.Split(char.Parse("{"));
        setInfoIndex = 0;
        foreach (string currentInfo in setInfo)
        {
            if (setInfoIndex == 0)
            {
                titleText.text = currentInfo;
                setInfoIndex++;
            }
            else if (setInfoIndex == 1)
            {
                setInfoIndex++;
            }
            else
            {
                RoundObjectPri = Instantiate(RoundObject, listPosForRound);
                roundCard = RoundObjectPri.GetComponent<RoundCard>();
                roundCard.roundFile = currentInfo;
                setInfoIndex2 = 0;
                setInfo2 = currentInfo.Split(char.Parse(":"));
                foreach (string currentInfo2 in setInfo2)
                {
                    if (setInfoIndex2 == 0)
                    {
                        roundCard.timeBtwWaves = currentInfo2;
                        setInfoIndex2++;
                    }
                    else if (setInfoIndex2 == 1)
                    {
                        roundCard.numToRepeat = currentInfo2;
                        roundCard.roundNum = setInfoIndex - 1;
                        roundCard.listPosForRound = listPosForRound;
                        roundCard.waveCreator = waveCreate;
                        roundCard.UpdateCard();
                        setInfoIndex2 = 0;
                        break;
                    }
                }
                setInfoIndex++;
            }
        }
    }
    public void CreateNewSet()
    {
        SetViewer.SetActive(false);
        Modebuilder.SetActive(true);
    }
    public void LoadSetLobby()
    {
        SetViewer.SetActive(true);
        Modebuilder.SetActive(false);
        RoundBuilder.SetActive(false);
        WaveBuilder.SetActive(false);
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.roundset");
        foreach (FileInfo file in info)
        {
            LobbyObjectPri = Instantiate(LobbyObject, listPosForSet);
            modeCard = LobbyObjectPri.GetComponent<ModeCard>();
            StreamReader reader = file.OpenText();
            loadableString = reader.ReadLine();
            reader.Close();
            setInfo = loadableString.Split(char.Parse("{"));
            foreach (string currentInfo in setInfo)
            {
                if (setInfoIndex == 0)
                {
                    modeCard.title = currentInfo;
                    modeCard.pathToDelete = Application.persistentDataPath + "/modesave" + currentInfo + ".scree";
                    modeCard.loadableString = loadableString;
                    modeCard.listPosForSet = listPosForSet;
                    modeCard.waveCreator = waveCreate;
                    setInfoIndex++;
                }
                else if (setInfoIndex == 1)
                {
                    modeCard.amOfRounds = currentInfo;
                    modeCard.UpdateCard();
                    setInfoIndex = 0;
                    break;
                }
            }
        }
    }
}
