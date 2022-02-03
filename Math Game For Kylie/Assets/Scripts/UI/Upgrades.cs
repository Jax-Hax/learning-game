using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    private TowerUpgradeScriptableObject currentTower;
    private string currentPopCount;
    [HideInInspector]
    public Penguin penguin;
    [Header("UI")]
    public TextMeshProUGUI currentTowerName;
    public Image towerImage;
    public TextMeshProUGUI popCount;
    public TextMeshProUGUI targeting;
    public Image[] upgradeLevels;
    public Sprite isUpgraded;
    public Sprite isNotUpgraded;
    public TextMeshProUGUI currentUpgradeText;
    public Image currentUpgradeImage;
    public TextMeshProUGUI nextUpgradeText;
    public Image nextUpgradeImage;
    public TextMeshProUGUI costOfNextUpgrade;
    public TextMeshProUGUI sellCost;
    public GameObject changePathMain;
    public GameObject changeToPath1;
    public GameObject changeToPath2;
    public GameObject changeToPath3;
    public GameObject upgradeMain;
    public GameManager gameManager;
    private int currentSellPrice;
    private int costOfNextUpgradeInt;
    private int upgradeLevel;
    private int upgradePath;
    private string scriptableObjectName;
    //public GameObject maxLevelMenu;
    public GameObject levelZeroDisplay;
    public GameObject currentUpgradeDisplay;
    public GameObject nextUpgradeDisplay;
    public GameObject maxLevelDisplay;
    public void OpenMenu(string towerName, int currentUpgradeLevel, int currentUpgradePath, string targetingType, TowerUpgradeScriptableObject scriptableObject)
    {
        upgradeMain.SetActive(true);
        gameManager.IsUpgradesTurnedOn();
        currentTowerName.text = towerName;
        scriptableObjectName = towerName;
        currentTower = scriptableObject;
        towerImage.sprite = currentTower.towerImage;
        targeting.text = targetingType;
        upgradePath = currentUpgradePath;
        upgradeLevel = currentUpgradeLevel;
        UpdateValues();
    }
    private void UpdateValues()
    {
        for (int i = 0; i < upgradeLevel; i++)
        {
            upgradeLevels[i].sprite = isUpgraded;
        }
        if (upgradePath == 1)
        {
            if(upgradeLevel == 0)
            {
                levelZeroDisplay.SetActive(true);
                currentUpgradeDisplay.SetActive(false);
                maxLevelDisplay.SetActive(false);
                nextUpgradeDisplay.SetActive(true);
                currentSellPrice = currentTower.path100SellPrices[upgradeLevel];
                nextUpgradeText.text = currentTower.path100Name[upgradeLevel];
                costOfNextUpgradeInt = currentTower.path100Cost[upgradeLevel];
                costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
                nextUpgradeImage.sprite = currentTower.path100Images[upgradeLevel];
            }
            else if(upgradeLevel >= 6)
            {
                Debug.Log("e");// ADD A THING THAT SAYS UR OUT OF MONEY and also add the paths and also add a description button to give a description
                currentUpgradeImage.sprite = currentTower.path100Images[upgradeLevel - 1];
                currentUpgradeText.text = currentTower.path100Name[upgradeLevel - 1];
                maxLevelDisplay.SetActive(true);
                nextUpgradeDisplay.SetActive(false);
                levelZeroDisplay.SetActive(false);
                currentUpgradeDisplay.SetActive(true);
            }
            else
            {
                currentUpgradeImage.sprite = currentTower.path100Images[upgradeLevel - 1];
                currentUpgradeText.text = currentTower.path100Name[upgradeLevel - 1];
                levelZeroDisplay.SetActive(false);
                maxLevelDisplay.SetActive(false);
                currentUpgradeDisplay.SetActive(true);
                nextUpgradeDisplay.SetActive(true);
                currentSellPrice = currentTower.path100SellPrices[upgradeLevel];
                nextUpgradeText.text = currentTower.path100Name[upgradeLevel];
                costOfNextUpgradeInt = currentTower.path100Cost[upgradeLevel];
                costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
                nextUpgradeImage.sprite = currentTower.path100Images[upgradeLevel];
            }
            sellCost.text = "$" + currentSellPrice.ToString();
        }
        else if (upgradePath == 2)
        {
            currentSellPrice = currentTower.path010SellPrices[upgradeLevel];
            currentUpgradeText.text = currentTower.path010Name[upgradeLevel];
            nextUpgradeText.text = currentTower.path100Name[upgradeLevel];
            costOfNextUpgradeInt = currentTower.path010Cost[upgradeLevel];
            costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
            sellCost.text = "$" + currentSellPrice.ToString();
            nextUpgradeImage.sprite = currentTower.path010Images[upgradeLevel + 1];
            currentUpgradeImage.sprite = currentTower.path010Images[upgradeLevel];
        }
        else if (upgradePath == 2)
        {
            currentSellPrice = currentTower.path001SellPrices[upgradeLevel];
            currentUpgradeText.text = currentTower.path001Name[upgradeLevel];
            nextUpgradeText.text = currentTower.path100Name[upgradeLevel];
            costOfNextUpgradeInt = currentTower.path001Cost[upgradeLevel];
            costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
            nextUpgradeImage.sprite = currentTower.path001Images[upgradeLevel + 1];
            currentUpgradeImage.sprite = currentTower.path001Images[upgradeLevel];
            sellCost.text = "$" + currentSellPrice.ToString();
        }
    }
    public void Upgrade()
    {
        if(gameManager.moneyInt >= costOfNextUpgradeInt && upgradeLevel != 6)
        {
            gameManager.UpdateMoney(-costOfNextUpgradeInt, true);
            if(scriptableObjectName == "penguin")
            {
                penguin.Upgrade();
            }
            upgradeLevel++;
            UpdateValues();
        }
    }
    public void OpenChangePath()
    {

    }
}
