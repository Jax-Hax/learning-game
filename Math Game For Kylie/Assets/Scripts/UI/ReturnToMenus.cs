using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ReturnToMenus : MonoBehaviour
{
    public GameObject moneyInput;
    public GameObject questionInput;
    public GameObject answerInput;
    public GameObject titleInput;
    public TMP_Dropdown dropDown;
    public Slider speed;
    public Slider moneyEarnedScale;
    public TMP_InputField startingCash;
    public TMP_InputField startingLives;
    public TMP_InputField rounds;
    public TMP_InputField abilityCooldownRate;
    public TMP_InputField bloonHealthMultiplier;
    private bool canChange;
    public TextMeshProUGUI speedText;
    void Start()
    {
        if (dropDown != null)
        {
            dropDown.onValueChanged.AddListener(delegate
            {
                ChangeValue();
            });
        }
    }
    public void ChangeValue()
    {
        switch (dropDown.value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
        canChange = false;
        Invoke("SetToTrue", 0.1f);
    }
    public void InputChanged(string name)
    {
        if(name == "speed")
        {
            speedText.text = speed.value.ToString();
        }
        if(canChange == true)
        {
            dropDown.value = 4;
        }
    }
    void SetToTrue()
    {
        canChange = true;
    }
    public void OnSelectInput(string whatInput)
    {
        if(whatInput == "money")
        {
            moneyInput.SetActive(false);
        }
        else if (whatInput == "question")
        {
            questionInput.SetActive(false);
        }
        else if (whatInput == "answer")
        {
            answerInput.SetActive(false);
        }
        else if (whatInput == "title")
        {
            titleInput.SetActive(false);
        }
    }
    public void OnDeselectInput(string whatInput)
    {
        if (whatInput == "money")
        {
            moneyInput.SetActive(true);
        }
        else if (whatInput == "question")
        {
            questionInput.SetActive(true);
        }
        else if (whatInput == "answer")
        {
            answerInput.SetActive(true);
        }
        else if (whatInput == "title")
        {
            titleInput.SetActive(true);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game Choice Screen");
    }
    public void CreateFlashcards()
    {
        SceneManager.LoadScene("Create Flashcard Scene");
    }
}
