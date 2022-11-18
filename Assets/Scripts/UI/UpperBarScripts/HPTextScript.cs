using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPTextScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    public void Start()
    {
        textComponent.text = GameManager.playerData.currentHP + "/" + GameManager.playerData.maxHP;
    }
}
