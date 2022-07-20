using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundCard : MonoBehaviour
{
    public string timeBtwWaves;
    public string numToRepeat;
    public TextMeshProUGUI roundsText;
    public Transform listPosForRound;
    public int roundNum;
    public string roundFile;
    public string pathToDelete;
    public WaveCreator waveCreator;
    public RoundSave roundSave;
    private int roundNumCopy;
    private GameObject obj;
    public void UpdateCard()
    {
        roundsText.text = roundNum.ToString();
    }
    public void DeleteSet()
    {
        waveCreator.setSave.RemoveAt(roundNum - 1);
        Destroy(gameObject);
    }
    public void DuplicateSet()
    {
        waveCreator.amOfRounds += 1;
        roundNumCopy = waveCreator.amOfRounds;
        waveCreator.setSave.Add(roundSave);
        obj = Instantiate(gameObject,gameObject.transform.parent);
        obj.GetComponent<RoundCard>().roundNum = roundNumCopy;
    }
    public void LoadSet()
    {
        waveCreator.DecodeRound(roundFile, roundNum, roundSave);
    }
}
