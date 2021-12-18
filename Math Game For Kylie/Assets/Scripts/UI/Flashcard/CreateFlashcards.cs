using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CreateFlashcards : MonoBehaviour
{
    FlashcardSet flashcardSet;
    private Flashcard flashcard;
    private int currentNumber;
    public GameObject chooseCards;
    public GameObject createCards;
    public TextMeshProUGUI questionNumber;
    public TMP_InputField title;
    public TextMeshProUGUI subject;
    public TMP_InputField questionText;
    public TMP_InputField answerText;
    public TMP_InputField moneyText;
    private int amOfCards;
    private string saveableString;
    private string loadableString;
    private string[] setInfo;
    private int setInfoIndex = 0;
    private int setDecodeIndex = 0;
    private string flashcardSeperator = "{";
    public GameObject lobbyGameobject;
    private GameObject lobbyObject;
    private LobbyCard lobbyCard;
    private string lobbyTitle;
    public Transform instantiatePosMath;
    public Transform instantiatePosScience;
    public Transform instantiatePosSS;
    public Transform instantiatePosEnglish;
    public Transform instantiatePosLanguage;
    public Transform instantiatePosOther;
    private List<GameObject> lobbyItemsToDelete = new List<GameObject>();
    private string questionName;
    private string answerName;
    private int moneyEarned;
    public List<GameObject> enabledCards = new List<GameObject>();
    public List<GameObject> disabledCards = new List<GameObject>();
    private void Start()
    {
        LoadSetLobby();
    }
    public void EnableACard()
    {
        foreach (GameObject objectBruh in enabledCards)
        {
            if(objectBruh != null)
            {
                objectBruh.SetActive(false);
            }
        }
        foreach (GameObject objectBruh in disabledCards)
        {
            if(objectBruh != null)
            {
                objectBruh.SetActive(true);
            }
        }
    }
    public void Next()
    {
        AddCard();
        currentNumber++;
        if (currentNumber > flashcardSet.flashcards.Count)
        {
            CreateNewCard();
        }
        else
        {
            LoadCard(flashcardSet.flashcards[currentNumber]);
        }
    }
    public void Back()
    {
        if (currentNumber != 1)
        {
            AddCard();
            currentNumber--;
            LoadCard(flashcardSet.flashcards[currentNumber]);
        }
    }
    public void AddCard()
    {
        flashcard = new Flashcard();
        if(answerText.text == "")
        {
            flashcard.answer = "Not Answered";
        }
        else
        {
            flashcard.answer = answerText.text;
        }
        if (questionText.text == "")
        {
            flashcard.question = "Not Answered";
        }
        else
        {
            flashcard.question = questionText.text;
        }
        if (moneyText.text == "")
        {
            flashcard.moneyGiven = 0;
        }
        else
        {
            flashcard.moneyGiven = int.Parse(moneyText.text);
        }
        if (flashcardSet.flashcards.ContainsKey(currentNumber))
        {
            flashcardSet.flashcards.Remove(currentNumber);
        }
        flashcardSet.flashcards.Add(currentNumber, flashcard);
    }
    public void CreateNewCard()
    {
        questionText.text = "";
        answerText.text = "";
        moneyText.text = "";
        flashcardSet.amOfCards += 1;
        questionNumber.text = "Flashcard #: " + currentNumber.ToString() + "/" + flashcardSet.amOfCards;
    }
    public void LoadCard(Flashcard flashcard)
    {
        questionText.text = flashcard.question;
        answerText.text = flashcard.answer;
        moneyText.text = flashcard.moneyGiven.ToString();
        questionNumber.text = "Flashcard #: " + currentNumber.ToString() + "/" + flashcardSet.amOfCards;
    }
    public void FinishSet()
    {
        AddCard();
        Save();
        LoadSetLobby();
    }
    public void LoadSetLobby()
    {
        chooseCards.SetActive(true);
        createCards.SetActive(false);
        foreach (GameObject itemToDelete in lobbyItemsToDelete)
        {
            Destroy(itemToDelete);
        }
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
                    if(currentInfo == "Math")
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
    }
    public void LoadPreviousSet()
    {
        chooseCards.SetActive(false);
        createCards.SetActive(true);
        currentNumber = 1;
        title.text = flashcardSet.nameOfSet;
        subject.text = flashcardSet.subject;
        LoadCard(flashcardSet.flashcards[1]);
    }
    public void DecodeSet(string set)
    {
        int currentCardNumber = 1;
        flashcardSet = new FlashcardSet();
        setInfo = set.Split(char.Parse("{"));
        foreach (string currentInfo in setInfo)
        {
            if (setDecodeIndex == 0)
            {
                flashcardSet.nameOfSet = currentInfo;
                setDecodeIndex++;
            }
            else if (setDecodeIndex == 1)
            {
                flashcardSet.subject = currentInfo;
                setDecodeIndex++;
            }
            else if (setDecodeIndex == 2)
            {
                flashcardSet.amOfCards = int.Parse(currentInfo);
                setDecodeIndex++;
            }
            else if (setDecodeIndex == 3)
            {
                questionName = currentInfo;
                setDecodeIndex++;
            }
            else if (setDecodeIndex == 4)
            {
                answerName = currentInfo;
                setDecodeIndex++;
            }
            else if (setDecodeIndex == 5)
            {
                moneyEarned = int.Parse(currentInfo);
                flashcard = new Flashcard();
                flashcard.question = questionName;
                flashcard.answer = answerName;
                flashcard.moneyGiven = moneyEarned;
                flashcardSet.flashcards.Add(currentCardNumber, flashcard);
                if(currentCardNumber == flashcardSet.amOfCards)
                {
                    setDecodeIndex = 0;
                    break;
                }
                else
                {
                    setDecodeIndex = 3;
                    currentCardNumber++;
                }
            }
        }
        LoadPreviousSet();
    }
    public void CreateNewSet(string topic)
    {
        chooseCards.SetActive(false);
        createCards.SetActive(true);
        flashcardSet = new FlashcardSet();
        subject.text = topic;
        flashcardSet.subject = topic;
        currentNumber = 1;
        CreateNewCard();
    }
    public void Save()
    {
        flashcardSet.nameOfSet = title.text;
        saveableString = flashcardSet.nameOfSet + flashcardSeperator + flashcardSet.subject + flashcardSeperator + flashcardSet.amOfCards + flashcardSeperator;
        foreach(KeyValuePair<int, Flashcard> keyValue in flashcardSet.flashcards)
        {
            saveableString += keyValue.Value.question + flashcardSeperator + keyValue.Value.answer + flashcardSeperator + keyValue.Value.moneyGiven + flashcardSeperator;
        }
        string path = Application.persistentDataPath + "/setSave" + title.text + ".scree";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        StreamWriter streamWriter = new StreamWriter(path);
        streamWriter.Write(saveableString);
        streamWriter.Close();
    }
}