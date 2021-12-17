using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Linq;

public class QuestionCreator : MonoBehaviour
{
    public GameObject NoSetEnabled;
    private string loadableString;
    public FlashcardSet set;
    private string[] setInfo;
    private int setDecodeIndex;
    private string questionName;
    private string answerName;
    private int moneyEarned;
    private Flashcard flashcard;
    private int currentCardNumber = 1;
    public GameObject questionGameobject;
    public GameObject correctAnswerGameobject;
    public GameObject wrongAnswerGameobject;
    public GameObject checkAnswerButton;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerText;
    public TextMeshProUGUI moneyEarnedText;
    public TMP_InputField answerInput;
    public GameManager gameManager;
    private bool newQuestion;
    private string path;
    private Dictionary<int, Flashcard> setSaved = new Dictionary<int, Flashcard>();
    private void Awake()
    {
        path = Application.persistentDataPath + "/currentSave.hecc";
        if (File.Exists(path))
        {
            questionGameobject.SetActive(true);
            newQuestion = true;
            NoSetEnabled.SetActive(false);
            NoSetEnabled.SetActive(false);
            StreamReader reader = new StreamReader(path);
            loadableString = reader.ReadLine();
            reader.Close();
            set = new FlashcardSet();
            Decoder();
        }
        else
        {
            newQuestion = false;
            NoSetEnabled.SetActive(true);
        }
    }
    public void NewQuestion()
    {
        if (!newQuestion)
        {
            return;
        }
        if (setSaved.Count <= 0)
        {
            Debug.Log("empy");
            setSaved = set.flashcards;
        }
        Debug.Log(set.flashcards.Count);
        answerInput.text = "";
        checkAnswerButton.SetActive(true);
        correctAnswerGameobject.SetActive(false);
        wrongAnswerGameobject.SetActive(false);
        List<int> keys = setSaved.Keys.ToList();
        int key = keys[Random.Range(0, setSaved.Count)];
        flashcard = setSaved[key];
        setSaved.Remove(key);
        questionText.text = flashcard.question;
        moneyEarnedText.text = "Worth: $" + flashcard.moneyGiven.ToString();
    }
    public void CheckAnswer()
    {
        checkAnswerButton.SetActive(false);
        if (answerInput.text == flashcard.answer)
        {
            correctAnswerGameobject.SetActive(true);
            gameManager.UpdateMoney(flashcard.moneyGiven, false);
        }
        else
        {
            wrongAnswerGameobject.SetActive(true);
            gameManager.UpdateMoney(-flashcard.moneyGiven, true);
        }
    }
    private void Decoder()
    {
        setInfo = loadableString.Split(char.Parse("{"));
        foreach (string currentInfo in setInfo)
        {
            if (setDecodeIndex == 0)
            {
                set.nameOfSet = currentInfo;
                setDecodeIndex++;
            }
            else if (setDecodeIndex == 1)
            {
                set.subject = currentInfo;
                setDecodeIndex++;
            }
            else if (setDecodeIndex == 2)
            {
                set.amOfCards = int.Parse(currentInfo);
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
                set.flashcards.Add(currentCardNumber, flashcard);
                if (currentCardNumber == set.amOfCards)
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
    }
}
