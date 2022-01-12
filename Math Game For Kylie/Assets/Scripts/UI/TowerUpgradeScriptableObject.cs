using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TowerUpgrade", menuName = "TowerUpgrade")]
public class TowerUpgradeScriptableObject : ScriptableObject
{
    [Header("General")]
    public Sprite towerImage;
    [Header("Path 100")]
    public Sprite[] path100Images;
    public GameObject[] path100Models;
    public int[] path100Cost;
    public int[] path100SellPrices;
    public string[] path100Name;
    [Header("Path 010")]
    public Sprite[] path010Images;
    public GameObject[] path010Models;
    public int[] path010Cost;
    public int[] path010SellPrices;
    public string[] path010Name;
    [Header("Path 001")]
    public Sprite[] path001Images;
    public GameObject[] path001Models;
    public int[] path001Cost;
    public int[] path001SellPrices;
    public string[] path001Name;
}
