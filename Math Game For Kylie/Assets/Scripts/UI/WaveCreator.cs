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
    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }/*
    public void LoadSetLobby()
    {
        SetViewer.SetActive(true);
        Modebuilder.SetActive(false);
        RoundBuilder.SetActive(false);
        WaveBuilder.SetActive(false);
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.scree");
        foreach (FileInfo file in info)
        {
            StreamReader reader = file.OpenText();
            loadableString = reader.ReadLine();
            reader.Close();
            setInfo = loadableString.Split(char.Parse("{"));
            foreach (string currentInfo in setInfo)
            {
                if (setInfoIndex == 0)
                {
                    lobbyTitle = currentInfo;
                    setInfoIndex++;
                }
                else if (setInfoIndex == 1)
                {
                    if (currentInfo == "Math")
                    {
                        lobbyObject = Instantiate(lobbyGameobject, instantiatePosMath);
                    }
                    else if (currentInfo == "Science")
                    {
                        lobbyObject = Instantiate(lobbyGameobject, instantiatePosScience);
                    }
                    else if (currentInfo == "History")
                    {
                        lobbyObject = Instantiate(lobbyGameobject, instantiatePosSS);
                    }
                    else if (currentInfo == "English")
                    {
                        lobbyObject = Instantiate(lobbyGameobject, instantiatePosEnglish);
                    }
                    else if (currentInfo == "Language")
                    {
                        lobbyObject = Instantiate(lobbyGameobject, instantiatePosLanguage);
                    }
                    else if (currentInfo == "Other")
                    {
                        lobbyObject = Instantiate(lobbyGameobject, instantiatePosOther);
                    }
                    lobbyItemsToDelete.Add(lobbyObject);
                    setInfoIndex++;
                }
                else if (setInfoIndex == 2)
                {
                    lobbyCard = lobbyObject.GetComponent<LobbyCard>();
                    lobbyCard.title = lobbyTitle;
                    lobbyCard.amOfCards = currentInfo;
                    lobbyCard.loadableString = loadableString;
                    lobbyCard.pathToDelete = Application.persistentDataPath + "/setSave" + lobbyTitle + ".scree";
                    lobbyCard.UpdateCard();
                    setInfoIndex = 0;
                    break;
                }
            }
        }
    }*/
}
