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
    public void UpdateCard()
    {
        roundsText.text = roundNum.ToString();
    }
    public void DeleteSet()
    {
        /*File.Delete(pathToDelete);
        Destroy(gameObject);*/
    }
    public void DuplicateSet()
    {
        /*newPathNum = 1;
        RoundObjectPri = Instantiate(RoundObject, listPosForRound);
        roundCard = RoundObjectPri.GetComponent<ModeCard>();
        while (File.Exists(pathToDelete + newPathNum))
        {
            newPathNum += 1;
        }
        modeCard.title = title + newPathNum;
        modeCard.pathToDelete = pathToDelete + newPathNum;
        modeCard.loadableString = loadableString;
        modeCard.amOfRounds = amOfRounds;
        modeCard.UpdateCard();
        StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/" + pathToDelete + newPathNum + ".roundSave");
        streamWriter.Write(loadableString);
        streamWriter.Close();*/
    }
    public void LoadSet()
    {
        waveCreator.DecodeRound(roundFile, roundNum);
    }
}
