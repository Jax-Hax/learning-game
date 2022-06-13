using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
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
    private int setInfoIndex = 0;
    public Transform listPosForSet;
    private WaveCreator waveCreate;
    private void Start()
    {
        LoadSetLobby();
        waveCreate = gameObject.GetComponent<WaveCreator>();
    }
    public void DecodeSet(string loadedString)
    {
        SetViewer.SetActive(false);
        Modebuilder.SetActive(true);
        setInfo = loadedString.Split(char.Parse("{"));
    }
    public void CreateNewSet()
    {
        SetViewer.SetActive(false);
        Modebuilder.SetActive(true);
        setInfoIndex = 0;
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
