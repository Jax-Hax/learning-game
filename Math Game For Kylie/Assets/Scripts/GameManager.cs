using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public Transform canvas;
    private int mapName;
    public GameObject[] maps;

    [System.NonSerialized]
    public List<GameObject> enemies = new List<GameObject>();

    public GameObject shop;
    public GameObject shopArrow;
    public GameObject shopTriangle;
    public Vector3 posForShopArrowWhileShopOpen;
    public Vector3 posForShopArrowWhileQuestionsOpen;
    public Vector3 posForShopArrowWhileNoneAreOpen;

    public GameObject questions;
    public GameObject questionsArrow;
    public GameObject questionsTriangle;
    public Vector3 posForQuestionsArrowWhileShopOpen;
    public Vector3 posForQuestionsArrowWhileQuestionsOpen;
    public Vector3 posForQuestionsArrowWhileNoneAreOpen;

    public RectTransform roundsTextPos;
    public Vector3 posForRoundsWhileNoneAreOpen;
    public Vector3 posForRoundsWhileShop;
    public Vector3 posForRoundsWhileQuestions;

    private bool isShopShowing;
    private bool isQuestionsShowing;
    public TextMeshProUGUI money;
    [System.NonSerialized]
    public int moneyInt;
    [System.NonSerialized]
    public int health;
    [System.NonSerialized]
    public GameObject prefab;
    public GameObject showableMoney;
    private string difficulty;
    [System.NonSerialized]
    public Transform[] mapPositions;
    private int moneyAddedOrTaken;
    private bool isAdding;
    private void Awake()
    {
        SetStartMoneyFromDifficulty();
        SetMap();
        PutArrowsInCorrectPlace();
        moneyAddedOrTaken = 0;
    }

    private void PutArrowsInCorrectPlace()
    {
        questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileNoneAreOpen;
        shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileNoneAreOpen;
        roundsTextPos.anchoredPosition =  posForRoundsWhileNoneAreOpen;
        isShopShowing = false;
        isQuestionsShowing = false;
        shop.SetActive(false);
        questions.SetActive(false);
    }

    private void SetMap()
    {
        mapName = PlayerPrefs.GetInt("MapName");
        prefab = Instantiate(maps[mapName]);
        prefab.transform.SetParent(canvas, false);
        prefab.transform.SetSiblingIndex(0);
        mapPositions = prefab.GetComponent<MapCode>().positions;
    }

    private void SetStartMoneyFromDifficulty()
    {
        difficulty = PlayerPrefs.GetString("DifficultyName");
        showableMoney.SetActive(false);
        if (difficulty == "DefaultEasy")
        {
            moneyInt = 1000000; //TODO: Change money back
        }
        money.text = moneyInt.ToString();
    }

    public void IsShopClicked()
    {
        questionsTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
        if (isShopShowing)
        {
            shopTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileNoneAreOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileNoneAreOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileNoneAreOpen;
            shop.SetActive(false);
            isShopShowing = false;
        }
        else
        {
            shopTriangle.transform.rotation = Quaternion.identity;
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileShopOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileShopOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileShop;
            shop.SetActive(true);
            questions.SetActive(false);
            isQuestionsShowing = false;
            isShopShowing = true;
        }
    }
    public void IsMoneyClicked()
    {
        shopTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
        if (isQuestionsShowing)
        {
            questionsTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileNoneAreOpen;
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileNoneAreOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileNoneAreOpen;
            questions.SetActive(false);
            isQuestionsShowing = false;
        }
        else
        {
            questionsTriangle.transform.rotation = Quaternion.identity;
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileQuestionsOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileQuestionsOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileQuestions;
            questions.SetActive(true);
            shop.SetActive(false);
            isShopShowing = false;
            isQuestionsShowing = true;
        }
    }
    public void UpdateMoney(int addedMoney, bool isNegative)
    {
        moneyInt += addedMoney;
        moneyAddedOrTaken += addedMoney;
        showableMoney.SetActive(true);
        if (Mathf.Sign(addedMoney) == -1)
        {
            if (isAdding)
            {
                moneyAddedOrTaken = addedMoney;
                isAdding = false;
            }
            showableMoney.GetComponent<TextMeshProUGUI>().text = moneyAddedOrTaken.ToString();
            showableMoney.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0.179f, 0f);
        }
        else if (addedMoney == 0)
        {
            if (isNegative)
            {
                showableMoney.GetComponent<TextMeshProUGUI>().text = "-0";
                showableMoney.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0.179f, 0f);
            }
            else
            {
                showableMoney.GetComponent<TextMeshProUGUI>().text = "+0";
                showableMoney.GetComponent<TextMeshProUGUI>().color = new Color(0.1223f, 1f, 0f);
            }
        }
        else
        {
            if (!isAdding)
            {
                moneyAddedOrTaken = addedMoney;
                isAdding = true;
            }
            showableMoney.GetComponent<TextMeshProUGUI>().text = "+" + moneyAddedOrTaken.ToString();
            showableMoney.GetComponent<TextMeshProUGUI>().color = new Color(0.1223f, 1f, 0f);
        }
        money.text = moneyInt.ToString();
        CancelInvoke();
        Invoke("Unactivate", 3);
    }
    public void WinLevel()
    {
        Debug.Log("You won!");
    }
    private void Unactivate()
    {
        moneyAddedOrTaken = 0;
        showableMoney.SetActive(false);
    }
    public void IsShopClickedTrueOrFalse(bool isTrue)
    {
        if (!isTrue)
        {
            shopTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileNoneAreOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileNoneAreOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileNoneAreOpen;
            shop.SetActive(false);
            isShopShowing = false;
        }
        else
        {
            shopTriangle.transform.rotation = Quaternion.identity;
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileShopOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileShopOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileShop;
            shop.SetActive(true);
            questions.SetActive(false);
            isQuestionsShowing = false;
            isShopShowing = true;
        }
    }
}
