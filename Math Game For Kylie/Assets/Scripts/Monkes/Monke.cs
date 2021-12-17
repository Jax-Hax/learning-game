using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Monke", menuName = "Monke")]
public class Monke : ScriptableObject
{
    public GameObject moveableMonke;
    public GameObject shootingMonke;
    public int cost;
    public Sprite monkePicture;
    public string monkeName;
}
