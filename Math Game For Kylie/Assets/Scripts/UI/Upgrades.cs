using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    private TowerUpgradeScriptableObject currentTower;
    private string currentPopCount;
    [System.NonSerialized]
    public DartMonke dart;
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
    public GameObject maxLevelMenu;
    public void OpenMenu(string towerName, int currentUpgradeLevel, int currentUpgradePath, string targetingType, TowerUpgradeScriptableObject scriptableObject)
    {
        upgradeMain.SetActive(true);
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
            currentSellPrice = currentTower.path100SellPrices[upgradeLevel - 2];
            currentUpgradeText.text = currentTower.path100Name[upgradeLevel - 2];
            nextUpgradeText.text = currentTower.path100Name[upgradeLevel - 1];
            costOfNextUpgradeInt = currentTower.path100Cost[upgradeLevel - 1];
            costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
            sellCost.text = "$" + currentSellPrice.ToString();
            nextUpgradeImage.sprite = currentTower.path100Images[upgradeLevel];
            currentUpgradeImage.sprite = currentTower.path100Images[upgradeLevel - 1];
        }
        else if (upgradePath == 2)
        {
            currentSellPrice = currentTower.path010SellPrices[upgradeLevel - 2];
            currentUpgradeText.text = currentTower.path010Name[upgradeLevel - 2];
            nextUpgradeText.text = currentTower.path100Name[upgradeLevel - 1];
            costOfNextUpgradeInt = currentTower.path010Cost[upgradeLevel - 1];
            costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
            sellCost.text = "$" + currentSellPrice.ToString();
            nextUpgradeImage.sprite = currentTower.path010Images[upgradeLevel];
            currentUpgradeImage.sprite = currentTower.path010Images[upgradeLevel - 1];
        }
        else if (upgradePath == 2)
        {
            currentSellPrice = currentTower.path001SellPrices[upgradeLevel - 2];
            currentUpgradeText.text = currentTower.path001Name[upgradeLevel - 2];
            nextUpgradeText.text = currentTower.path100Name[upgradeLevel - 1];
            costOfNextUpgradeInt = currentTower.path001Cost[upgradeLevel - 1];
            costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
            nextUpgradeImage.sprite = currentTower.path001Images[upgradeLevel];
            currentUpgradeImage.sprite = currentTower.path001Images[upgradeLevel - 1];
            sellCost.text = "$" + currentSellPrice.ToString();
        }
    }
    public void Upgrade()
    {
        if(gameManager.moneyInt >= costOfNextUpgradeInt)
        {
            gameManager.UpdateMoney(-costOfNextUpgradeInt, true);
            if(scriptableObjectName == "dart")
            {
                dart.Upgrade();
                upgradeLevel++;
                Debug.Log(upgradeLevel);
                UpdateValues();
            }
            if(upgradeLevel >= 6)
            {
                Debug.Log("e");
            }
        }
    }
    public void OpenChangePath()
    {

    }
}
