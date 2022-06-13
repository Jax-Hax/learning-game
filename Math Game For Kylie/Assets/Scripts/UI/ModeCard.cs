using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ModeCard : MonoBehaviour
{
    public string loadableString;
    public string title;
    public string amOfRounds;
    private string path;
    private string currentSaveString;
    public GameObject isEnabled;
    public GameObject isNotEnabled;
    public string pathToDelete;
    public TextMeshProUGUI amOfRoundsText;
    public TextMeshProUGUI titleText;
    public GameObject LobbyObject;
    private GameObject LobbyObjectPri;
    private ModeCard modeCard;
    public Transform listPosForSet;
    public WaveCreator waveCreator;
    private int newPathNum;
    private void Start()
    {
        path = Application.persistentDataPath + "/currentSave.roundSave";
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            currentSaveString = reader.ReadLine();
            reader.Close();
            if (currentSaveString == loadableString)
            {
                isEnabled.SetActive(true);
                isNotEnabled.SetActive(false);
            }
        }
    }
    public void UpdateCard()
    {
        amOfRoundsText.text = amOfRounds + " Rounds";
        titleText.text = title;
    }
    public void ChooseSet()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        StreamWriter streamWriter = new StreamWriter(path);
        streamWriter.Write(loadableString);
        streamWriter.Close();
        isEnabled.SetActive(false);
        isNotEnabled.SetActive(true);
    }
    public void UnchooseSet()
    {
        File.Delete(path);
        isEnabled.SetActive(true);
        isNotEnabled.SetActive(false);
    }
    public void DeleteSet()
    {
        File.Delete(pathToDelete);
        Destroy(gameObject);
    }
    public void DuplicateSet()
    {
        newPathNum = 1;
        LobbyObjectPri = Instantiate(LobbyObject, listPosForSet);
        modeCard = LobbyObjectPri.GetComponent<ModeCard>();
        while(File.Exists(pathToDelete + newPathNum))
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
        streamWriter.Close();
    }
    public void LoadSet()
    {
        waveCreator.DecodeSet(loadableString);
    }
}
