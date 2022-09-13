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
    public GameObject answerGameobject;
    public GameObject checkAnswerButton;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerText;
    public TextMeshProUGUI moneyEarnedText;
    public TMP_InputField answerInput;
    public TextMeshProUGUI moneyEarnedFromQuestionText;
    public TextMeshProUGUI isAnswerRightText;
    public GameManager gameManager;
    private bool newQuestion;
    private string path;
    private bool studiedAllBool = false;
    public GameObject studiedAll;
    private string flashcardAnswer;
    private int flashcardMoney;
    public float moneyMult;
    public TextAsset defaultQuestions;
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
            loadableString = defaultQuestions.text;
            StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/currentSave.hecc");
            streamWriter.Write(loadableString);
            streamWriter.Close();
            streamWriter = new StreamWriter(Application.persistentDataPath + "/defaultMultiplicationQuestions.scree");
            streamWriter.Write(loadableString);
            streamWriter.Close();
        }
    }
    public void NewQuestion()
    {
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
        answerGameobject.SetActive(false);
        List<int> keys = set.flashcards.Keys.ToList();
        int key = keys[Random.Range(0, set.flashcards.Count)];
        flashcard = set.flashcards[key];
        set.flashcards.Remove(key);
        questionText.text = flashcard.question;
        flashcardAnswer = flashcard.answer;
        flashcardMoney = Mathf.RoundToInt(flashcard.moneyGiven * (moneyMult / 100));
        moneyEarnedText.text = "Worth: $" + flashcardMoney.ToString();
        if (set.flashcards.Count <= 0)
        {
            studiedAllBool = true;
            Decoder();
        }
    }
    public void CheckAnswer()
    {
        checkAnswerButton.SetActive(false);
        if (answerInput.text == flashcardAnswer)
        {
            answerGameobject.SetActive(true);
            answerText.text = flashcardAnswer;
            gameManager.UpdateMoney(flashcardMoney, false);
            isAnswerRightText.text = "Correct!";
            isAnswerRightText.color = new Color(0.1223f, 1f, 0f);
            moneyEarnedFromQuestionText.text = "+" + flashcardMoney.ToString();
            moneyEarnedFromQuestionText.color = new Color(0.1223f, 1f, 0f);

        }
        else
        {
            answerGameobject.SetActive(true);
            answerText.text = flashcardAnswer;
            gameManager.UpdateMoney(-flashcardMoney, true);
            isAnswerRightText.text = "Incorrect!";
            isAnswerRightText.color = new Color(1f, 0.179f, 0f);
            moneyEarnedFromQuestionText.text =  "-" + flashcardMoney.ToString();
            moneyEarnedFromQuestionText.color = new Color(1f, 0.179f, 0f);
        }
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
