using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class ReadAndSpawnWaves : MonoBehaviour
{
    private List<Round> rounds = new List<Round>();
    private int maxRound;
    private Transform whereToSpawnThem;
    public TextAsset textFile;
    private string[] allRounds;
    private string[] allWaves;
    private int parseIndex;
    private int waveParseIndex;
    public int currentRoundNumber = 0;
    private Round round;
    private Wave wave;
    private string[] allPartsOfWave;
    private int howManyTimesToRepeat;
    public TextMeshProUGUI roundText;
    private GameManager gameManager;
    public GameObject questionCreator;
    private string saveFile;
    private string[] setInfo;
    private int setInfoIndex;
    private float speedMult;
    private float healthMult;
    private float moneyMult;
    private bool isCamo;
    private BloonCode blooncode;
    private string textToParse;
    private int index2;
    private int maxRound2;
    private void Start()
    {
        StreamReader reader = new StreamReader(Application.persistentDataPath + "/" + "saveFile.saveFile");
        saveFile = reader.ReadLine();
        reader.Close();
        setInfo = saveFile.Split(char.Parse("}"));
        setInfoIndex = 0;
        foreach (string currentInfo in setInfo)
        {
            if (setInfoIndex == 0)
            {
                maxRound = int.Parse(currentInfo);
            }
            if (setInfoIndex == 1)
            {
                speedMult = float.Parse(currentInfo) / 100;
            }
            if (setInfoIndex == 2)
            {
                healthMult = float.Parse(currentInfo)  / 100;
            }
            if (setInfoIndex == 3)
            {
                moneyMult = float.Parse(currentInfo);
            }
            if (setInfoIndex == 6)
            {
                isCamo = Convert.ToBoolean(int.Parse(currentInfo));
            }
            setInfoIndex++;
        }
        questionCreator.GetComponent<QuestionCreator>().moneyMult = moneyMult;
        ParseFile();
        whereToSpawnThem = GameObject.FindGameObjectWithTag("SpawnPos").transform;
        whereToSpawnThem.position = new Vector3(whereToSpawnThem.position.x, whereToSpawnThem.position.y, 0);
        gameManager = GameManager.SharedInstance;
    }
    public void StartGame()
    {
        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        foreach (Round round in rounds)
        {
            while (gameManager.enemies.Count != 0) { yield return new WaitForSeconds(0.5f); }
            currentRoundNumber++;
            roundText.text = currentRoundNumber + "/" + maxRound;
            howManyTimesToRepeat = round.howManyTimesToRepeat;
            for (int j = 0; j < howManyTimesToRepeat; j++)
            {
                foreach (Wave wave in round.waves)
                {
                    for (int i = 0; i < wave.howManyBloons; i++)
                    {
                        GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(wave.bloonType);
                        bloon.SetActive(true);
                        bloon.transform.position = whereToSpawnThem.position;
                        blooncode = bloon.GetComponent<BloonCode>();
                        blooncode.wayPointIndex = 0;
                        blooncode.healthMult = healthMult;
                        blooncode.speedMult = speedMult;
                        blooncode.roundNum = currentRoundNumber;
                        gameManager.enemies.Add(bloon);
                        yield return new WaitForSeconds(wave.timeBetweenBloons);
                    }
                    yield return new WaitForSeconds(round.timeBetweenEachWave);
                }
            }
        }
        gameManager.WinLevel();
    }
    private void ParseFile()
    {
        if(File.Exists(Application.persistentDataPath + "/currentSave.roundSave"))
        {
            StreamReader reader = new StreamReader(Application.persistentDataPath + "/currentSave.roundSave");
            textToParse = reader.ReadLine();
            reader.Close();
        }
        else
        {
            textToParse = textFile.text;
            StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/currentSave.roundSave");
            streamWriter.Write(textToParse);
            streamWriter.Close();
            streamWriter = new StreamWriter(Application.persistentDataPath + "/defaultrounds.roundset");
            streamWriter.Write(textToParse);
            streamWriter.Close();
        }
        allRounds = textToParse.Split(char.Parse("{"));
        parseIndex = 0;
        index2 = 0;
        foreach (string currentRound in allRounds)
        {
            if(index2 == 1)
            {
                maxRound2 = Mathf.RoundToInt(Mathf.Clamp(maxRound, 1, float.Parse(currentRound)));
                roundText.text = "0/" + maxRound2;
                maxRound = maxRound2;
            }
            if (index2 >= 2)
            {
                round = new Round();
                allWaves = currentRound.Split(char.Parse(":"));
                foreach (string currentWave in allWaves)
                {
                    if (parseIndex == 0)
                    {
                        round.timeBetweenEachWave = float.Parse(currentWave);
                        parseIndex++;
                    }
                    else if (parseIndex == 1)
                    {
                        round.howManyTimesToRepeat = int.Parse(currentWave);
                        parseIndex++;
                    }
                    else
                    {
                        wave = new Wave();
                        allPartsOfWave = currentWave.Split(char.Parse(","));
                        foreach (string currentPartOfWave in allPartsOfWave)
                        {
                            if (waveParseIndex == 0)
                            {
                                wave.bloonType = int.Parse(currentPartOfWave);
                                waveParseIndex++;
                            }
                            else if (waveParseIndex == 1)
                            {
                                wave.timeBetweenBloons = float.Parse(currentPartOfWave);
                                waveParseIndex++;
                            }
                            else if (waveParseIndex == 2)
                            {
                                wave.isCamo = Convert.ToBoolean(int.Parse(currentPartOfWave));
                                waveParseIndex++;
                            }
                            else if (waveParseIndex == 3)
                            {
                                wave.howManyBloons = int.Parse(currentPartOfWave);
                                waveParseIndex = 0;
                                round.waves.Add(wave);
                            }
                        }
                    }
                }
                parseIndex = 0;
                rounds.Add(round);
            }
            index2++;
        }
    }
}
