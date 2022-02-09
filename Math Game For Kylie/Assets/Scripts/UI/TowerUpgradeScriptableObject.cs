using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TowerUpgrade", menuName = "TowerUpgrade")]
public class TowerUpgradeScriptableObject : ScriptableObject
{
    [Header("General")]
    public Sprite towerImage;
    public int costToUpgradeToPath1;
    public int costToUpgradeToPath2;
    public int costToUpgradeToPath3;
    [Header("Path 100")]
    public Sprite[] path100Images;
    public int[] path100Cost;
    public int[] path100SellPrices;
    public string[] path100Name;
    [Header("Path 010")]
    public Sprite[] path010Images;
    public int[] path010Cost;
    public int[] path010SellPrices;
    public string[] path010Name;
    [Header("Path 001")]
    public Sprite[] path001Images;
    public int[] path001Cost;
    public int[] path001SellPrices;
    public string[] path001Name;
}
