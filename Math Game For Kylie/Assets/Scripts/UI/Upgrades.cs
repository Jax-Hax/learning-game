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
    public TextMeshProUGUI changeToPath1Text;
    public TextMeshProUGUI changeToPath2Text;
    public TextMeshProUGUI changeToPath3Text;
    public GameObject upgradeMain;
    public GameManager gameManager;
    private int currentSellPrice;
    private int costOfNextUpgradeInt;
    private int upgradeLevel;
    private int upgradePath;
    private string scriptableObjectName;
    public GameObject levelZeroDisplay;
    public GameObject currentUpgradeDisplay;
    public GameObject nextUpgradeDisplay;
    public GameObject maxLevelDisplay;
    private int costForPath1;
    private int costForPath2;
    private int costForPath3;
    private int costToTakeOff;
    public GameObject firstChoice;
    public GameObject titleForChoosePath;
    public GameObject titleForChooseAnotherPath;
    public GameObject XButton;
    public void OpenMenu(string towerName, int currentUpgradeLevel, int currentUpgradePath, string targetingType, TowerUpgradeScriptableObject scriptableObject, int monkesPopCount)
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
        popCount.text = monkesPopCount.ToString();
        UpdateValues();
    }
    private void UpdateValues()
    {
        changePathMain.SetActive(false);
        firstChoice.SetActive(false);
        titleForChoosePath.SetActive(false);
        upgradeMain.SetActive(true);
        XButton.SetActive(true);
        titleForChooseAnotherPath.SetActive(true);
        for (int i = 0; i < upgradeLevel; i++)
        {
            upgradeLevels[i].color = Color.green;
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
                // ADD A THING THAT SAYS UR OUT OF MONEY and also add a description button to give a description, seperate upgrades from other stuff and make it switch sides based on where monke is, multiplying cost based on difficulty ALL AFTER ITS OUT
                currentUpgradeImage.sprite = currentTower.path100Images[upgradeLevel - 1];
                currentUpgradeText.text = currentTower.path100Name[upgradeLevel - 1];
                currentSellPrice = currentTower.path100SellPrices[upgradeLevel];
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
            if (upgradeLevel == 0)
            {
                levelZeroDisplay.SetActive(true);
                currentUpgradeDisplay.SetActive(false);
                maxLevelDisplay.SetActive(false);
                nextUpgradeDisplay.SetActive(true);
                currentSellPrice = currentTower.path010SellPrices[upgradeLevel];
                nextUpgradeText.text = currentTower.path010Name[upgradeLevel];
                costOfNextUpgradeInt = currentTower.path010Cost[upgradeLevel];
                costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
                nextUpgradeImage.sprite = currentTower.path010Images[upgradeLevel];
            }
            else if (upgradeLevel >= 6)
            {
                // ADD A THING THAT SAYS UR OUT OF MONEY and also add a description button to give a description, seperate upgrades from other stuff and make it switch sides based on where monke is, multiplying cost based on difficulty ALL AFTER ITS OUT
                currentUpgradeImage.sprite = currentTower.path010Images[upgradeLevel - 1];
                currentUpgradeText.text = currentTower.path010Name[upgradeLevel - 1];
                currentSellPrice = currentTower.path010SellPrices[upgradeLevel];
                maxLevelDisplay.SetActive(true);
                nextUpgradeDisplay.SetActive(false);
                levelZeroDisplay.SetActive(false);
                currentUpgradeDisplay.SetActive(true);
            }
            else
            {
                currentUpgradeImage.sprite = currentTower.path010Images[upgradeLevel - 1];
                currentUpgradeText.text = currentTower.path010Name[upgradeLevel - 1];
                levelZeroDisplay.SetActive(false);
                maxLevelDisplay.SetActive(false);
                currentUpgradeDisplay.SetActive(true);
                nextUpgradeDisplay.SetActive(true);
                currentSellPrice = currentTower.path010SellPrices[upgradeLevel];
                nextUpgradeText.text = currentTower.path010Name[upgradeLevel];
                costOfNextUpgradeInt = currentTower.path010Cost[upgradeLevel];
                costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
                nextUpgradeImage.sprite = currentTower.path010Images[upgradeLevel];
            }
            sellCost.text = "$" + currentSellPrice.ToString();
        }
        else if (upgradePath == 3)
        {
            if (upgradeLevel == 0)
            {
                levelZeroDisplay.SetActive(true);
                currentUpgradeDisplay.SetActive(false);
                maxLevelDisplay.SetActive(false);
                nextUpgradeDisplay.SetActive(true);
                currentSellPrice = currentTower.path001SellPrices[upgradeLevel];
                nextUpgradeText.text = currentTower.path001Name[upgradeLevel];
                costOfNextUpgradeInt = currentTower.path001Cost[upgradeLevel];
                costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
                nextUpgradeImage.sprite = currentTower.path001Images[upgradeLevel];
            }
            else if (upgradeLevel >= 6)
            {
                // ADD A THING THAT SAYS UR OUT OF MONEY and also add a description button to give a description, seperate upgrades from other stuff and make it switch sides based on where monke is, multiplying cost based on difficulty ALL AFTER ITS OUT, ability to crosspatgh for money
                currentUpgradeImage.sprite = currentTower.path001Images[upgradeLevel - 1];
                currentUpgradeText.text = currentTower.path001Name[upgradeLevel - 1];
                currentSellPrice = currentTower.path001SellPrices[upgradeLevel];
                maxLevelDisplay.SetActive(true);
                nextUpgradeDisplay.SetActive(false);
                levelZeroDisplay.SetActive(false);
                currentUpgradeDisplay.SetActive(true);
            }
            else
            {
                currentUpgradeImage.sprite = currentTower.path001Images[upgradeLevel - 1];
                currentUpgradeText.text = currentTower.path001Name[upgradeLevel - 1];
                levelZeroDisplay.SetActive(false);
                maxLevelDisplay.SetActive(false);
                currentUpgradeDisplay.SetActive(true);
                nextUpgradeDisplay.SetActive(true);
                currentSellPrice = currentTower.path001SellPrices[upgradeLevel];
                nextUpgradeText.text = currentTower.path001Name[upgradeLevel];
                costOfNextUpgradeInt = currentTower.path001Cost[upgradeLevel];
                costOfNextUpgrade.text = "$" + costOfNextUpgradeInt.ToString();
                nextUpgradeImage.sprite = currentTower.path001Images[upgradeLevel];
            }
            sellCost.text = "$" + currentSellPrice.ToString();
        }
        else if(upgradePath == 4)
        {
            changePathMain.SetActive(true);
            firstChoice.SetActive(true);
            titleForChoosePath.SetActive(true);
            upgradeMain.SetActive(false);
            XButton.SetActive(false);
            titleForChooseAnotherPath.SetActive(false);
            changeToPath1Text.text = "Path 1: $" + currentTower.costToUpgradeToPath1.ToString();
            changeToPath2Text.text = "Path 2: $" + currentTower.costToUpgradeToPath2.ToString();
            changeToPath3Text.text = "Path 3: $" + currentTower.costToUpgradeToPath3.ToString();
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
        CalculateCostToChangePaths();
        if(upgradePath == 1)
        {
            changeToPath1.SetActive(false);
        }
        else if (upgradePath == 2)
        {
            changeToPath2.SetActive(false);
        }
        else if (upgradePath == 3)
        {
            changeToPath3.SetActive(false);
        }
    }
    public void CloseUpgrades()
    {
        if (scriptableObjectName == "penguin" && penguin != null)
        {
            penguin.HideRange();
        }
    }
    public void OpenUpgrades()
    {
        if (scriptableObjectName == "penguin")
        {
            penguin.ShowRange();
        }
    }
    public void CloseChangePath()
    {
        changeToPath1.SetActive(true);
        changeToPath2.SetActive(true);
        changeToPath3.SetActive(true);
        changePathMain.SetActive(false);
        firstChoice.SetActive(false);
        titleForChoosePath.SetActive(false);
        upgradeMain.SetActive(true);
        XButton.SetActive(true);
        titleForChooseAnotherPath.SetActive(true);
    }
    public void CalculateCostToChangePaths()
    {
        for (int i = 0; i < upgradeLevel; i++)
        {
            costForPath1 += currentTower.path100Cost[i];
        }
        for (int i = 0; i < upgradeLevel; i++)
        {
            costForPath2 += currentTower.path010Cost[i];
        }
        for (int i = 0; i < upgradeLevel; i++)
        {
            costForPath3 += currentTower.path001Cost[i];
        }
        if(upgradePath == 1)
        {
            costToTakeOff = costForPath1;
        }
        else if (upgradePath == 2)
        {
            costToTakeOff = costForPath2;
        }
        else if (upgradePath == 3)
        {
            costToTakeOff = costForPath3;
        }
        costForPath1 -= Mathf.FloorToInt(costToTakeOff * 1.2f) + currentTower.costToUpgradeToPath1;
        costForPath2 -= Mathf.FloorToInt(costToTakeOff * 1.2f) + currentTower.costToUpgradeToPath2;
        costForPath3 -= Mathf.FloorToInt(costToTakeOff * 1.2f) + currentTower.costToUpgradeToPath3;
        if (Mathf.Sign(costForPath1) == -1)
        {
            costForPath1 = 1000;
        }
        if (Mathf.Sign(costForPath2) == -1)
        {
            costForPath2 = 1000;
        }
        if (Mathf.Sign(costForPath3) == -1)
        {
            costForPath3 = 1000;
        }
        changeToPath3Text.text = "Path 3: $" + costForPath3.ToString();
        changeToPath2Text.text = "Path 2: $" + costForPath2.ToString();
        changeToPath1Text.text = "Path 1: $" + costForPath1.ToString();
    }
    public void ChangeToPath(int path)
    {
        if(upgradePath == 4)
        {
            if (path == 1)
            {
                if (gameManager.moneyInt >= currentTower.costToUpgradeToPath1)
                {
                    gameManager.UpdateMoney(-currentTower.costToUpgradeToPath1, true);
                    upgradePath = 1;
                    if(scriptableObjectName == "penguin")
                    {
                        penguin.upgradePath = 1;
                    }
                }
            }
            else if (path == 2)
            {
                if (gameManager.moneyInt >= currentTower.costToUpgradeToPath2)
                {
                    gameManager.UpdateMoney(-currentTower.costToUpgradeToPath2, true);
                    upgradePath = 2;
                    if (scriptableObjectName == "penguin")
                    {
                        penguin.upgradePath = 2;
                    }
                }
            }
            else if (path == 3)
            {
                if (gameManager.moneyInt >= currentTower.costToUpgradeToPath3)
                {
                    gameManager.UpdateMoney(-currentTower.costToUpgradeToPath3, true);
                    upgradePath = 3;
                    if (scriptableObjectName == "penguin")
                    {
                        penguin.upgradePath = 3;
                    }
                }
            }
            CloseChangePath();
            UpdateValues();
        }
        else
        {
            if (path == 1)
            {
                if (gameManager.moneyInt >= costForPath1)
                {
                    gameManager.UpdateMoney(-costForPath1, true);
                    upgradePath = 1;
                }
            }
            else if (path == 2)
            {
                if (gameManager.moneyInt >= costForPath2)
                {
                    gameManager.UpdateMoney(-costForPath2, true);
                    upgradePath = 2;
                }
            }
            else if (path == 3)
            {
                if (gameManager.moneyInt >= costForPath3)
                {
                    gameManager.UpdateMoney(-costForPath3, true);
                    upgradePath = 3;
                }
            }
            if (scriptableObjectName == "penguin")
            {
                penguin.upgradePath = upgradePath;
            }
            CloseChangePath();
            UpdateValues();
        }
    }
    public void Sell()
    {
        if(upgradePath == 1)
        {
            gameManager.UpdateMoney(currentTower.path100SellPrices[upgradeLevel], true);
        }
        else if(upgradePath == 2)
        {
            gameManager.UpdateMoney(currentTower.path010SellPrices[upgradeLevel], true);
        }
        else if (upgradePath == 3)
        {
            gameManager.UpdateMoney(currentTower.path001SellPrices[upgradeLevel], true);
        }
        if (scriptableObjectName == "penguin")
        {
            Destroy(penguin.gameObject);
            gameManager.IsUpgradesTurnedOff(false);
        }
        penguin = null;
    }
}
