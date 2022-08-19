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
    public Dictionary<int, RoundSave> setSave = new Dictionary<int, RoundSave>();
    private RoundSave roundSave;
    private WaveSave waveSave;
    public int amOfRounds;
    public int amOfRoundNum;
    private int curWaveNum;
    private string roundSaveCurrent;
    private string result;
    private string title;
    private int rounds;
    public string waveFruitType = "0";
    bool didMakeAWave;
    public Image bloonTypeImage;
    private string roundSaveSaveThing;
    private void Start()
    {
        LoadSetLobby();
        waveCreate = gameObject.GetComponent<WaveCreator>();
    }
    public void SetPlantType(string typeofPlant)
    {
        waveFruitType = typeofPlant;
        bloonTypeImage.sprite = bloonTypes[int.Parse(waveFruitType)];
    }
    public void LoadWave(string waveFile, int waveNum, GameObject obj)
    {
        curWaveNum = waveNum;
        Destroy(obj);
        RoundBuilder.SetActive(false);
        WaveBuilder.SetActive(true);
        setInfo = waveFile.Split(char.Parse(","));
        setInfoIndex = 0;
        foreach (string currentInfo in setInfo)
        {
            if (setInfoIndex == 0)
            {
                waveFruitType = currentInfo;
                bloonTypeImage.sprite = bloonTypes[int.Parse(waveFruitType)];
            }
            else if (setInfoIndex == 1)
            {
                timeBtwBloonsTextWave.text = currentInfo;
            }
            else if (setInfoIndex == 2)
            {
                toggleCamo.isOn = Convert.ToBoolean(int.Parse(currentInfo));
            }
            else if (setInfoIndex == 3)
            {
                numOfBloons.text = currentInfo;
            }
            setInfoIndex++;
        }
        setInfoIndex = 0;
    }
    public void SaveWave()
    {
        didMakeAWave = true;
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
        waveSave.fruitType = waveFruitType;
        if(amOfRoundNum == 0)
        {
            roundSave = new RoundSave();
            roundSave.waveSaves[curWaveNum] = waveSave;
        }
        else
        {
            roundSave.waveSaves[curWaveNum] = waveSave;
        }
        RoundBuilder.SetActive(true);
        WaveBuilder.SetActive(false);
        WaveObjectPri = Instantiate(WaveObject, listPosForWave);
        WaveObjectPri.transform.SetSiblingIndex(curWaveNum);
        waveCard = WaveObjectPri.GetComponent<WaveCard>();
        waveCard.waveFile = waveSave.fruitType + "," + waveSave.timeBtw + "," + waveSave.isCamo + "," + waveSave.amToSpawn;
        waveCard.isCamo = Convert.ToBoolean(int.Parse(waveSave.isCamo));
        waveCard.waveSave = waveSave;
        waveCard.waveNum = curWaveNum;
        waveCard.howMany = waveSave.amToSpawn;
        waveCard.waveCreator = waveCreate;
        waveCard.bloonType = bloonTypes[int.Parse(waveSave.fruitType)];
        waveCard.UpdateCard();
        numOfBloons.text = "";
        timeBtwBloonsTextWave.text = "";
        toggleCamo.isOn = false;
    }
    public void CreateWave()
    {
        didMakeAWave = true;
        RoundBuilder.SetActive(false);
        WaveBuilder.SetActive(true);
        numOfBloons.text = "";
        timeBtwBloonsTextWave.text = "";
        toggleCamo.isOn = false;
        if(amOfRoundNum == 0)
        {
            curWaveNum = 1;
        }
        else
        {
            curWaveNum = roundSave.waveSaves.Count + 1;
        }
    }
    public void SaveRound()
    {
        if (didMakeAWave == false)
        {
            roundSave = new RoundSave();
        }
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
        if(amOfRoundNum == 0)
        {
            setSave[1] =  roundSave;
        }
        else
        {
            setSave[amOfRoundNum] = roundSave;
        }
        Modebuilder.SetActive(true);
        RoundBuilder.SetActive(false);
        RoundObjectPri = Instantiate(RoundObject, listPosForRound);
        RoundObjectPri.transform.SetSiblingIndex(amOfRoundNum - 1);
        roundCard = RoundObjectPri.GetComponent<RoundCard>();
        roundSaveCurrent = roundSave.timeBtwWaves + ":" + roundSave.amToRepeat;
        if(roundSave.waveSaves.Count > 0)
        {
            foreach (WaveSave save in roundSave.waveSaves.Values)
            {
                roundSaveCurrent += ":" + save.fruitType + "," + save.timeBtw + "," + save.isCamo + "," + save.amToSpawn;
            }
            result = roundSaveCurrent.Substring(roundSaveCurrent.Length - 4);
            if (result == ":,,,")
            {
                roundSaveCurrent = roundSaveCurrent.Remove(roundSaveCurrent.Length - 4);
            }
        }
        roundCard.roundFile = roundSaveCurrent;
        roundSave.roundSave = roundSaveCurrent;
        roundCard.numToRepeat = roundSave.amToRepeat;
        roundCard.roundNum = amOfRoundNum;
        roundCard.listPosForRound = listPosForRound;
        roundCard.waveCreator = waveCreate;
        roundCard.timeBtwWaves = roundSave.timeBtwWaves;
        roundCard.roundSave = roundSave;
        roundCard.UpdateCard();
        timeBtwWavesText.text = "";
        amToRepeatText.text = "";
        roundSave = new RoundSave();
    }
    public void DecodeRound(string loadedString, int roundNum, RoundSave roundSave2)
    {
        didMakeAWave = true;
        setInfoIndex = 0;
        foreach(Transform child in listPosForWave)
        {
            if(setInfoIndex != 0)
            {
                Destroy(child.gameObject);
            }
            setInfoIndex++;
        }
        curWaveNum = roundSave2.waveSaves.Count + 1;
        amOfRoundNum = roundNum;
        Destroy(listPosForRound.GetChild(amOfRoundNum - 1).gameObject);
        Modebuilder.SetActive(false);
        RoundBuilder.SetActive(true);
        roundSave = roundSave2;
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
            else if (setInfoIndex == 1)
            {
                amToRepeatText.text = currentInfo;
                setInfoIndex++;
            }
            else if(setInfoIndex >= 2)
            {
                
                WaveObjectPri = Instantiate(WaveObject, listPosForWave);
                waveCard = WaveObjectPri.GetComponent<WaveCard>();
                waveCard.waveFile = currentInfo;
                setInfoIndex2 = 0;
                setInfo2 = currentInfo.Split(char.Parse(","));
                foreach (string currentInfo2 in setInfo2)
                {
                    if (setInfoIndex2 == 0)
                    {
                        waveCard.bloonType = bloonTypes[int.Parse(currentInfo2)];
                        setInfoIndex2++;
                    }
                    else if (setInfoIndex2 == 1)
                    {
                        setInfoIndex2++;
                    }
                    else if (setInfoIndex2 == 2)
                    {
                        waveCard.isCamo = Convert.ToBoolean(int.Parse(currentInfo2));
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
                setInfoIndex++;
            }
        }
        setInfoIndex = 0;
    }
    public void CreateNewRound()
    {
        /*if(!setSave.Any())
        {
            roundSave = new RoundSave();
            waveSave = new WaveSave();
            roundSave.setSave.Add(waveSave);
            setSave.Add(roundSave);
        }*/
        curWaveNum = 1;
        if(setSave.Count == 0)
        {
            amOfRoundNum = 1;
            roundSave = new RoundSave();
        }
        else
        {
            amOfRoundNum = setSave.Count + 1;
        }
        Modebuilder.SetActive(false);
        RoundBuilder.SetActive(true);
        waveTitle.text = "Round " + amOfRoundNum;
        timeBtwWavesText.text = "";
        amToRepeatText.text = "";
        setInfoIndex = 0;
        foreach (Transform child in listPosForWave)
        {
            if (setInfoIndex != 0)
            {
                Destroy(child.gameObject);
            }
            setInfoIndex++;
        }
    }
    public void SaveSet()
    {
        if (titleText.text != "")
        {
            title = titleText.text;
        }
        else
        {
            title = "Untitled";
        }
        Modebuilder.SetActive(false);
        SetViewer.SetActive(true);
        LobbyObjectPri = Instantiate(LobbyObject, listPosForSet);
        modeCard = LobbyObjectPri.GetComponent<ModeCard>();
        roundSaveCurrent = title + "{" + setSave.Count;
        rounds = setSave.Count;
        foreach (RoundSave save in setSave.Values)
        {
            roundSaveCurrent += "{" + save.roundSave;
        }
        modeCard.title = title;
        modeCard.amOfRounds = rounds.ToString();
        modeCard.loadableString = roundSaveCurrent;
        modeCard.pathToDelete = Application.persistentDataPath + "/" + title + ".roundset";
        StreamWriter streamWriter = new StreamWriter(modeCard.pathToDelete);
        streamWriter.Write(roundSaveCurrent);
        streamWriter.Close();
        modeCard.listPosForSet = listPosForSet;
        modeCard.waveCreator = waveCreate;
        modeCard.UpdateCard();
    }
    public void DecodeSet(string loadedString, int numb)
    {
        foreach (Transform child in listPosForRound)
        {
            Destroy(child.gameObject);
        }
        Destroy(listPosForSet.GetChild(numb).gameObject);
        SetViewer.SetActive(false);
        Modebuilder.SetActive(true);
        setInfo = loadedString.Split(char.Parse("{"));
        setInfoIndex = 0;
        setSave = new Dictionary<int, RoundSave>();
        foreach (string currentInfo in setInfo)
        {
            if (setInfoIndex == 0)
            {
                titleSave = currentInfo;
                titleText.text = currentInfo;
                setInfoIndex++;
            }
            else if(setInfoIndex == 1)
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
                        roundSaveSaveThing = currentInfo2;
                    }
                    else if (setInfoIndex2 == 1)
                    {
                        roundCard.numToRepeat = currentInfo2;
                        roundSave.amToRepeat = currentInfo2;
                        roundSaveSaveThing += ":" + currentInfo2;
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
                                roundSaveSaveThing += ":" + currentInfo3 + ",";
                                waveSave.fruitType = currentInfo3;
                            }
                            else if(setInfoIndex3 == 1)
                            {
                                roundSaveSaveThing += currentInfo3 + ",";
                                waveSave.timeBtw = currentInfo3;
                            }
                            else if (setInfoIndex3 == 2)
                            {
                                roundSaveSaveThing += currentInfo3 + ",";
                                waveSave.isCamo = currentInfo3;
                            }
                            else if (setInfoIndex3 == 3)
                            {
                                waveSave.amToSpawn = currentInfo3;
                                roundSaveSaveThing += currentInfo3;
                            }
                            setInfoIndex3++;
                        }
                        roundSave.waveSaves[setInfoIndex2 - 1] = waveSave;
                        setInfoIndex2++;
                    }
                }
                roundSave.roundSave = roundSaveSaveThing;
                amOfRounds = setInfoIndex2 - 1;
                setSave[setInfoIndex - 1] = roundSave;
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
        setInfoIndex2 = 0;
        foreach (FileInfo file in info)
        {
            LobbyObjectPri = Instantiate(LobbyObject, listPosForSet);
            modeCard = LobbyObjectPri.GetComponent<ModeCard>();
            StreamReader reader = file.OpenText();
            loadableString = reader.ReadLine();
            reader.Close();
            setInfo = loadableString.Split(char.Parse("{"));
            modeCard.number = setInfoIndex2;
            foreach (string currentInfo in setInfo)
            {
                if (setInfoIndex == 0)
                {
                    modeCard.title = currentInfo;
                    modeCard.pathToDelete = Application.persistentDataPath + "/" + currentInfo + ".roundset";
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
            setInfoIndex2++;
        }
        setInfoIndex2 = 0;
    }
}
