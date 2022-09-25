using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerItem : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    Image backgroundImage;
    public Color highlightColor;
    public void SetPlayerInfo(Player player2)
    {
        playerName.text = player2.NickName;
        backgroundImage = GetComponent<Image>();
    }
    public void ApplyLocalChanges()
    {
        backgroundImage.color = highlightColor;
    }
}
