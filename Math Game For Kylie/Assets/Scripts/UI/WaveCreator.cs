using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
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
    private int setInfoIndex3 = 0;
    private string[] setInfo3;
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
    private string titleSave;
    public List<RoundSave> setSave = new List<RoundSave>();
    private RoundSave roundSave;
    private WaveSave waveSave;
    public int amOfRounds;
    public int currentNum;
    private int curWaveNum;
    private string roundSaveCurrent;
    private string result;
    private void Start()
    {
        LoadSetLobby();
        waveCreate = gameObject.GetComponent<WaveCreator>();
    }
    public void LoadWave(string waveFile, int waveNum)
    {
        curWaveNum = waveNum;
        setSave[currentNum - 1].setSave.RemoveAt(waveNum - 1);
        Destroy(listPosForWave.GetChild(waveNum).gameObject);
        RoundBuilder.SetActive(false);
        WaveBuilder.SetActive(true);
        setInfo = waveFile.Split(char.Parse(","));
        setInfoIndex = 0;
        foreach (string currentInfo in setInfo)
        {
            if (setInfoIndex == 0)
            {
                //waveCard.bloonType = bloonTypes[int.Parse(currentInfo)];
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
    public void SaveWave()
    {
        waveSave = new WaveSave();
        if(numOfBloons.text != "")
        {
            waveSave.amToSpawn = numOfBloons.text;
        }
        else
        {
            waveSave.amToSpawn = "1";
        }
        if (timeBtwBloonsTextWave.text != "")
        {
            waveSave.timeBtw = timeBtwBloonsTextWave.text;
        }
        else
        {
            waveSave.timeBtw = "1";
        }
        waveSave.isCamo = Convert.ToInt32(toggleCamo.isOn).ToString();
        waveSave.fruitType = "0";
        setSave[currentNum - 1].setSave.Insert(curWaveNum - 1, waveSave);
        RoundBuilder.SetActive(true);
        WaveBuilder.SetActive(false);
        WaveObjectPri = Instantiate(WaveObject, listPosForWave);
        waveCard = WaveObjectPri.GetComponent<WaveCard>();
        waveCard.waveFile = waveSave.fruitType + "," + waveSave.timeBtw + "," + waveSave.isCamo + "," + waveSave.amToSpawn;
        waveCard.isCamo = Convert.ToBoolean(int.Parse(waveSave.isCamo));
        waveCard.waveNum = curWaveNum;
        waveCard.howMany = waveSave.amToSpawn;
        waveCard.waveCreator = waveCreate;
        waveCard.bloonType = bloonTypes[int.Parse(waveSave.fruitType)];
        waveCard.UpdateCard();
    }
    public void CreateWave()
    {
        RoundBuilder.SetActive(false);
        WaveBuilder.SetActive(true);
        curWaveNum = setSave[currentNum - 1].setSave.Count;
    }
    public void SaveRound()
    {
        if (timeBtwWavesText.text != "")
        {
            roundSave.timeBtwWaves = timeBtwWavesText.text;
        }
        else
        {
            roundSave.timeBtwWaves = "1";
        }
        if (amToRepeatText.text != "")
        {
            roundSave.amToRepeat = amToRepeatText.text;
        }
        else
        {
            roundSave.amToRepeat = "1";
        }
        setSave.Insert(currentNum - 1, roundSave);
        Modebuilder.SetActive(true);
        RoundBuilder.SetActive(false);
        RoundObjectPri = Instantiate(RoundObject, listPosForRound);
        roundCard = RoundObjectPri.GetComponent<RoundCard>();
        roundSaveCurrent = roundSave.timeBtwWaves + ":" + roundSave.amToRepeat;
        foreach(WaveSave save in roundSave.setSave)
        {
            roundSaveCurrent += ":" + save.fruitType + "," + save.timeBtw + "," + save.isCamo + "," + save.amToSpawn;
        }
        result = roundSaveCurrent.Substring(roundSaveCurrent.Length - 4);
        if(result == ":,,,")
        {
            roundSaveCurrent = roundSaveCurrent.Remove(roundSaveCurrent.Length - 4);
        }
        roundCard.roundFile = roundSaveCurrent;
        roundCard.numToRepeat = roundSave.amToRepeat;
        roundCard.roundNum = currentNum;
        roundCard.listPosForRound = listPosForRound;
        roundCard.waveCreator = waveCreate;
        roundCard.timeBtwWaves = roundSave.timeBtwWaves;
        roundCard.UpdateCard();
    }
    public void DecodeRound(string loadedString, int roundNum)
    {
        Debug.Log(loadedString);
        currentNum = roundNum;
        setSave.RemoveAt(currentNum - 1);
        Destroy(listPosForRound.GetChild(currentNum - 1).gameObject);
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
            else if(setInfoIndex2 >= 2)
            {
                WaveObjectPri = Instantiate(WaveObject, listPosForWave);
                waveCard = WaveObjectPri.GetComponent<WaveCard>();
                waveCard.waveFile = currentInfo;
                Debug.Log(currentInfo);
                setInfo2 = currentInfo.Split(char.Parse(","));
                foreach (string currentInfo2 in setInfo2)
                {
                    if(setInfoIndex2 == 0)
                    {
                        Debug.Log(currentInfo2);
                        waveCard.bloonType = bloonTypes[int.Parse(currentInfo2)];
                        setInfoIndex2++;
                    }
                    else if (setInfoIndex2 == 2)
                    {
                        waveCard.isCamo = Convert.ToBoolean(currentInfo2);
                        waveCard.waveNum = setInfoIndex - 1;
                        setInfoIndex2++;
                    }
                    else if(setInfoIndex2 == 3)
                    {
                        waveCard.howMany = currentInfo2;
                        waveCard.waveCreator = waveCreate;
                        waveCard.UpdateCard();
                        setInfoIndex2++;
                    }
                }
                setInfoIndex2 = 0;
            }
        }
        setInfoIndex = 0;
    }
    public void CreateNewRound()
    {
        if(!setSave.Any())
        {
            roundSave = new RoundSave();
            waveSave = new WaveSave();
            roundSave.setSave.Add(waveSave);
            setSave.Add(roundSave);
        }
        currentNum = setSave.Count;
        Modebuilder.SetActive(false);
        RoundBuilder.SetActive(true);
    }
    public void SaveSet()
    {

    }
    public void DecodeSet(string loadedString)
    {
        SetViewer.SetActive(false);
        Modebuilder.SetActive(true);
        setInfo = loadedString.Split(char.Parse("{"));
        setInfoIndex = 0;
        setSave = new List<RoundSave>();
        foreach (string currentInfo in setInfo)
        {
            if (setInfoIndex == 0)
            {
                titleSave = currentInfo;
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
                roundSave = new RoundSave();
                setInfo2 = currentInfo.Split(char.Parse(":"));
                foreach (string currentInfo2 in setInfo2)
                {
                    if (setInfoIndex2 == 0)
                    {
                        roundCard.timeBtwWaves = currentInfo2;
                        setInfoIndex2++;
                        roundSave.timeBtwWaves = currentInfo2;
                    }
                    else if (setInfoIndex2 == 1)
                    {
                        roundCard.numToRepeat = currentInfo2;
                        roundSave.amToRepeat = currentInfo2;
                        roundCard.roundNum = setInfoIndex - 1;
                        roundCard.listPosForRound = listPosForRound;
                        roundCard.waveCreator = waveCreate;
                        roundCard.UpdateCard();
                        setInfoIndex2++;
                    }
                    else
                    {
                        setInfo3 = currentInfo2.Split(char.Parse(","));
                        setInfoIndex3 = 0;
                        waveSave = new WaveSave();
                        foreach (string currentInfo3 in setInfo3)
                        {
                            if (setInfoIndex3 == 0)
                            {
                                waveSave.fruitType = currentInfo3;
                            }
                            else if(setInfoIndex3 == 1)
                            {
                                waveSave.timeBtw = currentInfo3;
                            }
                            else if (setInfoIndex3 == 2)
                            {
                                waveSave.isCamo = currentInfo3;
                            }
                            else if (setInfoIndex3 == 3)
                            {
                                waveSave.amToSpawn = currentInfo3;
                            }
                            setInfoIndex3++;
                        }
                        roundSave.setSave.Add(waveSave);
                        setInfoIndex2++;
                    }
                }
                amOfRounds = setInfoIndex2 - 1;
                setSave.Add(roundSave);
                roundCard.roundSave = roundSave;
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
