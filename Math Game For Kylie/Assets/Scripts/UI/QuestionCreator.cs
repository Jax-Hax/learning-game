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
    private bool studiedAllBool = false;
    public GameObject studiedAll;
    public GameObject correctAnswerText;
    private void Awake()
    {
        studiedAll.SetActive(false);
        path = Application.persistentDataPath + "/currentSave.hecc";
        if (File.Exists(path))
        {
            questionGameobject.SetActive(true);
            newQuestion = true;
            NoSetEnabled.SetActive(false);
            StreamReader reader = new StreamReader(path);
            loadableString = reader.ReadLine();
            reader.Close();
            setInfo = loadableString.Split(char.Parse("{"));
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
        correctAnswerText.SetActive(false);
        if (studiedAllBool)
        {
            studiedAll.SetActive(true);
            studiedAllBool = false;
        }
        else
        {
            studiedAll.SetActive(false);
        }
        if (!newQuestion)
        {
            return;
        }
        answerInput.text = "";
        checkAnswerButton.SetActive(true);
        correctAnswerGameobject.SetActive(false);
        wrongAnswerGameobject.SetActive(false);
        List<int> keys = set.flashcards.Keys.ToList();
        int key = keys[Random.Range(0, set.flashcards.Count)];
        flashcard = set.flashcards[key];
        set.flashcards.Remove(key);
        questionText.text = flashcard.question;
        moneyEarnedText.text = "Worth: $" + flashcard.moneyGiven.ToString();
        if (set.flashcards.Count <= 0)
        {
            studiedAllBool = true;
            Decoder();
        }
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
    public void CheckRightAnswer()
    {
        correctAnswerText.SetActive(true);
        answerText.text = flashcard.answer;
    }
    private void Decoder()
    {
        setDecodeIndex = 0;
        currentCardNumber = 0;
        set = new FlashcardSet();
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
                    currentCardNumber = 0;
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
