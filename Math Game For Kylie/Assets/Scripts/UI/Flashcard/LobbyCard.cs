using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class LobbyCard : MonoBehaviour
{
    public string title;
    public string amOfCards;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI amOfCardsText;
    public string loadableString;
    private string currentSaveString;
    public CreateFlashcards createFlashcards;
    public GameObject isItEnabled;
    public GameObject notenabled;
    private string path;
    private void Start()
    {
        path = Application.persistentDataPath + "/currentSave.hecc";
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            currentSaveString = reader.ReadLine();
            reader.Close();
            if(currentSaveString == loadableString)
            {
                isItEnabled.SetActive(true);
                notenabled.SetActive(false);
            }
        }
        createFlashcards = GameObject.FindGameObjectWithTag("CreateFlashcards").GetComponent<CreateFlashcards>();
        createFlashcards.enabledCards.Add(isItEnabled);
        createFlashcards.disabledCards.Add(notenabled);
    }
    public void UpdateCard()
    {
        if(int.Parse(amOfCards) == 1)
        {
            amOfCardsText.text = amOfCards + " Card";
        }
        else
        {
            amOfCardsText.text = amOfCards + " Cards";
        }
        titleText.text = title;
    }
    public void LoadSet()
    {
        createFlashcards.DecodeSet(loadableString);
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
        createFlashcards.EnableACard();
        isItEnabled.SetActive(true);
    }
    public void UnchooseSet()
    {
        createFlashcards.EnableACard();
        File.Delete(path);
    }
}
