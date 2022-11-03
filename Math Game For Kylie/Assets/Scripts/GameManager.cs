using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
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
    public Vector3 posForShopArrowWhileUpgradesAreOpen;

    public GameObject questions;
    public GameObject questionsArrow;
    public GameObject questionsTriangle;
    public Vector3 posForQuestionsArrowWhileShopOpen;
    public Vector3 posForQuestionsArrowWhileQuestionsOpen;
    public Vector3 posForQuestionsArrowWhileNoneAreOpen;
    public Vector3 posForQuestionsArrowWhileUpgradesAreOpen;
    public GameObject upgradesTriangle;
    public Vector3 posForUpgradesArrowWhileShopOpen;
    public Vector3 posForUpgradesArrowWhileQuestionsOpen;
    public Vector3 posForUpgradesArrowWhileNoneAreOpen;
    public Vector3 posForUpgradesArrowWhileUpgradesAreOpen;

    public RectTransform roundsTextPos;
    public Vector3 posForRoundsWhileNoneAreOpen;
    public Vector3 posForRoundsWhileShop;
    public Vector3 posForRoundsWhileQuestions;
    public Vector3 posForRoundsWhileUpgrades;
    public GameObject upgradeArrow;
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
    public Vector3[] mapPositions;
    private int moneyAddedOrTaken;
    private bool isAdding;
    public TextMeshProUGUI healthText;
    public List<Vector3> mapWaypoints = new List<Vector3>();
    public GameObject upgrades;
    public GameObject choosePath;
    public Upgrades upgradeMain;
    private string saveFile;
    private string[] setInfo;
    private int setInfoIndex;
    public bool won;
    private float amToGoDownPerTime;
    public GameObject WinningObject;
    public GameObject LosingObject;
    public List<Range> ranges = new List<Range>();

    //Abilities
    //Penguin
    public List<Penguin> penguins = new List<Penguin>();
    bool penguinT5p1 = false;
    bool penguinT5p2 = false;
    bool penguinT5p3 = false;
    [HideInInspector]
    public List<Penguin> haveChild1 = new List<Penguin>();
    public GameObject haveChild1Button;
    public TextMeshProUGUI haveChild1Text;
    public GameObject haveChild1Slider;
    [HideInInspector]
    public List<Penguin> haveChild2 = new List<Penguin>();
    public GameObject haveChild2Button;
    public TextMeshProUGUI haveChild2Text;
    public GameObject haveChild2Slider;
    [HideInInspector]
    public List<Penguin> haveChild3 = new List<Penguin>();
    public GameObject haveChild3Button;
    public TextMeshProUGUI haveChild3Text;
    public GameObject haveChild3Slider;
    [HideInInspector]
    public List<Penguin> ultraPeck = new List<Penguin>();
    public GameObject ultraPeckButton;
    public TextMeshProUGUI ultraPeckText;
    public GameObject ultraPeckSlider;

    private void Awake()
    {
        StreamReader reader = new StreamReader(Application.persistentDataPath + "/" + "saveFile.saveFile");
        saveFile = reader.ReadLine();
        reader.Close();
        SetStartMoneyFromDifficulty();
        SetMap();
        PutArrowsInCorrectPlace();
        moneyAddedOrTaken = 0;
        upgradeArrow.SetActive(false);
        SharedInstance = this;
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
        foreach (Transform pos in prefab.GetComponent<MapCode>().positions)
        {
            mapWaypoints.Add(new Vector3(pos.position.x, pos.position.y, 0));
        }
        mapPositions = mapWaypoints.ToArray();
    }

    private void SetStartMoneyFromDifficulty()
    {
        setInfo = saveFile.Split(char.Parse("}"));
        setInfoIndex = 0;
        foreach (string currentInfo in setInfo)
        {
            if (setInfoIndex == 4)
            {
                moneyInt = int.Parse(currentInfo);
            }
            if (setInfoIndex == 5)
            {
                health = int.Parse(currentInfo);
            }
            setInfoIndex++;
        }
        showableMoney.SetActive(false);
        money.text = moneyInt.ToString();
        healthText.text = health.ToString();
    }

    public void IsShopClicked()
    {
        questions.SetActive(false);
        upgrades.SetActive(false);
        questionsTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
        upgradesTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
        if (isShopShowing)
        {
            shopTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileNoneAreOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileNoneAreOpen;
            upgradeArrow.GetComponent<RectTransform>().anchoredPosition = posForUpgradesArrowWhileNoneAreOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileNoneAreOpen;
            shop.SetActive(false);
            isShopShowing = false;
        }
        else
        {
            shopTriangle.transform.rotation = Quaternion.identity;
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileShopOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileShopOpen;
            upgradeArrow.GetComponent<RectTransform>().anchoredPosition = posForUpgradesArrowWhileShopOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileShop;
            shop.SetActive(true);
            questions.SetActive(false);
            isQuestionsShowing = false;
            isShopShowing = true;
        }
    }
    public void IsUpgradesTurnedOn()
    {
        upgrades.SetActive(true);
        upgradeMain.OpenUpgrades();
        upgradeArrow.SetActive(true);
        shopTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
        upgradesTriangle.transform.rotation = Quaternion.identity;
        questionsTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
        shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileUpgradesAreOpen;
        questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileUpgradesAreOpen;
        upgradeArrow.GetComponent<RectTransform>().anchoredPosition = posForUpgradesArrowWhileUpgradesAreOpen;
        roundsTextPos.anchoredPosition = posForRoundsWhileUpgrades;
        shop.SetActive(false);
        questions.SetActive(false);
        isQuestionsShowing = false;
        isShopShowing = false;
    }
    public void IsUpgradesTurnedOff(bool isPerma)
    {
        if(!upgrades.activeSelf)
        {
            IsUpgradesTurnedOn();
        }
        else
        {
            upgrades.SetActive(false);
            upgradeMain.CloseUpgrades();
            upgradesTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileNoneAreOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileNoneAreOpen;
            upgradeArrow.GetComponent<RectTransform>().anchoredPosition = posForUpgradesArrowWhileNoneAreOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileNoneAreOpen;
            shop.SetActive(false);
            questions.SetActive(false);
            isQuestionsShowing = false;
            isShopShowing = false;
        }
        if(isPerma == false)
        {
            upgradeArrow.SetActive(false);
        }
    }
    public void UpdateHealth(int healthTaken)
    {
        health += healthTaken;
        healthText.text = health.ToString();
        if(health <= 0)
        {
            LoseGame();
        }
    }
    public void IsMoneyClicked()
    {
        upgrades.SetActive(false);
        upgradesTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
        shopTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
        if (isQuestionsShowing)
        {
            questionsTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileNoneAreOpen;
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileNoneAreOpen;
            upgradeArrow.GetComponent<RectTransform>().anchoredPosition = posForUpgradesArrowWhileNoneAreOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileNoneAreOpen;
            questions.SetActive(false);
            isQuestionsShowing = false;
            isShopShowing = false;
        }
        else
        {
            questionsTriangle.transform.rotation = Quaternion.identity;
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileQuestionsOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileQuestionsOpen;
            upgradeArrow.GetComponent<RectTransform>().anchoredPosition = posForUpgradesArrowWhileQuestionsOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileQuestions;
            questions.SetActive(true);
            shop.SetActive(false);
            isShopShowing = false;
            isQuestionsShowing = true;
        }
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void GameMenu()
    {
        SceneManager.LoadScene("Game Choice Screen");
    }
    public void LoseGame()
    {
        LosingObject.SetActive(true);
        Time.timeScale = 0;
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
        StartCoroutine("CheckIfWinning");
    }
    public IEnumerator CheckIfWinning()
    {
        while (enemies.Count > 0)
        {
            yield return null;
        }
        WinningObject.SetActive(true);
        Time.timeScale = 0;
    }
    private void Unactivate()
    {
        moneyAddedOrTaken = 0;
        showableMoney.SetActive(false);
    }
    public void IsShopClickedTrueOrFalse(bool isTrue)
    {
        upgrades.SetActive(false);
        upgradesTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
        if (!isTrue)
        {
            shopTriangle.transform.rotation = new Quaternion(0, 0, -180, 0);
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileNoneAreOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileNoneAreOpen;
            upgradeArrow.GetComponent<RectTransform>().anchoredPosition = posForUpgradesArrowWhileNoneAreOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileNoneAreOpen;
            shop.SetActive(false);
            isShopShowing = false;
        }
        else
        {
            shopTriangle.transform.rotation = Quaternion.identity;
            shopArrow.GetComponent<RectTransform>().anchoredPosition = posForShopArrowWhileShopOpen;
            questionsArrow.GetComponent<RectTransform>().anchoredPosition = posForQuestionsArrowWhileShopOpen;
            upgradeArrow.GetComponent<RectTransform>().anchoredPosition = posForUpgradesArrowWhileShopOpen;
            roundsTextPos.anchoredPosition = posForRoundsWhileShop;
            shop.SetActive(true);
            questions.SetActive(false);
            isQuestionsShowing = false;
            isShopShowing = true;
        }
    }
    public void UseAbility(string abilityName)
    {
        switch (abilityName)
        {
            case "haveChild1":
                haveChild1[0].HaveChild1();
                haveChild1.RemoveAt(0);
                if (haveChild1.Count == 0)
                {
                    haveChild1Text.text = "1";
                    haveChild1Button.GetComponent<Button>().interactable = false;
                    haveChild1Slider.SetActive(true);
                    haveChild1Slider.GetComponent<AbilitySlider>().SetCooldown(45);
                }
                else
                {
                    haveChild1Text.text = haveChild1.Count.ToString();
                }
                break;
            case "haveChild2":
                haveChild2[0].HaveChild2();
                haveChild2.RemoveAt(0);
                if (haveChild2.Count == 0)
                {
                    haveChild2Text.text = "1";
                    haveChild2Button.GetComponent<Button>().interactable = false;
                    haveChild2Slider.SetActive(true);
                    haveChild2Slider.GetComponent<AbilitySlider>().SetCooldown(45);
                }
                else
                {
                    haveChild2Text.text = haveChild2.Count.ToString();
                }
                break;
            case "haveChild3":
                haveChild3[0].HaveChild3();
                haveChild3.RemoveAt(0);
                if (haveChild3.Count == 0)
                {
                    haveChild3Text.text = "1";
                    haveChild3Button.GetComponent<Button>().interactable = false;
                    haveChild3Slider.SetActive(true);
                    haveChild3Slider.GetComponent<AbilitySlider>().SetCooldown(30);
                }
                else
                {
                    haveChild3Text.text = haveChild3.Count.ToString();
                }
                break;
            case "ultraPeck":
                ultraPeck[0].UltraPeck();
                ultraPeck.RemoveAt(0);
                if (ultraPeck.Count == 0)
                {
                    ultraPeckText.text = "1";
                    ultraPeckButton.GetComponent<Button>().interactable = false;
                    ultraPeckSlider.SetActive(true);
                    ultraPeckSlider.GetComponent<AbilitySlider>().SetCooldown(30);
                }
                else
                {
                    ultraPeckText.text = ultraPeck.Count.ToString();
                }
                break;
        }
    }
    public void CheckAbility(string abilityName)
    {
        switch (abilityName)
        {
            case "haveChild1":
                haveChild1Button.SetActive(true);
                if (haveChild1Button.GetComponent<Button>().interactable == false)
                {
                    haveChild1Button.GetComponent<Button>().interactable = true;
                    haveChild1Slider.SetActive(false);
                }
                else
                {
                    haveChild1Text.text = haveChild1.Count.ToString();
                }
                break;
            case "haveChild2":
                haveChild2Button.SetActive(true);
                if (haveChild2Button.GetComponent<Button>().interactable == false)
                {
                    haveChild2Button.GetComponent<Button>().interactable = true;
                    haveChild2Slider.SetActive(false);
                }
                else
                {
                    haveChild2Text.text = haveChild2.Count.ToString();
                }
                break;
            case "haveChild3":
                haveChild3Button.SetActive(true);
                if (haveChild3Button.GetComponent<Button>().interactable == false)
                {
                    haveChild3Button.GetComponent<Button>().interactable = true;
                    haveChild3Slider.SetActive(false);
                }
                else
                {
                    haveChild3Text.text = haveChild3.Count.ToString();
                }
                break;
            case "ultraPeck":
                ultraPeckButton.SetActive(true);
                if (ultraPeckButton.GetComponent<Button>().interactable == false)
                {
                    ultraPeckButton.GetComponent<Button>().interactable = true;
                    ultraPeckSlider.SetActive(false);
                }
                else
                {
                    ultraPeckText.text = ultraPeck.Count.ToString();
                }
                break;
        }
    }
    public void RemoveBloonFromRange(GameObject bloon)
    {
        foreach(Range range in ranges)
        {
            range.activeList.Remove(bloon);
            range.notActiveList.Remove(bloon);
        }
    }
}
