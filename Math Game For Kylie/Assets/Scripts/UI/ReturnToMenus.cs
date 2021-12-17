using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReturnToMenus : MonoBehaviour
{
    public GameObject moneyInput;
    public GameObject questionInput;
    public GameObject answerInput;
    public GameObject titleInput;
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
