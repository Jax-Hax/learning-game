using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadAndSpawnWaves : MonoBehaviour
{
    private List<Round> rounds = new List<Round>();
    private string difficulty;
    private int maxRound;
    private Transform whereToSpawnThem;
    public TextAsset textFile;
    private string[] allRounds;
    private string[] allWaves;
    private int parseIndex;
    private int waveParseIndex;
    private int currentRoundNumber = 0;
    private Round round;
    private Wave wave;
    private string[] allPartsOfWave;
    private int howManyTimesToRepeat;
    public TextMeshProUGUI roundText;
    private GameManager gameManager;
    private void Start()
    {
        difficulty = PlayerPrefs.GetString("DifficultyName");
        if (difficulty == "DefaultEasy")
        {
            maxRound = 40;
        }
        ParseFile();
        whereToSpawnThem = GameObject.FindGameObjectWithTag("SpawnPos").transform;
        whereToSpawnThem.position = new Vector3(whereToSpawnThem.position.x, whereToSpawnThem.position.y, 0);
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    public void StartGame()
    {
        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        foreach (Round round in rounds)
        {
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
                        bloon.GetComponent<BloonCode>().wayPointIndex = 0;
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
        allRounds = textFile.text.Split(char.Parse("|"));
        parseIndex = 0;
        foreach (string currentRound in allRounds)
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
                    allPartsOfWave = currentWave.Split(char.Parse("-"));
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
                            wave.isCamo = Convert.ToBoolean(currentPartOfWave);
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
    }
}
