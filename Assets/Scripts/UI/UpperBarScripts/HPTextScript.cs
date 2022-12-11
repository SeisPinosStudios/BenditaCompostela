using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HPTextScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "BattleScene") textComponent.text = GameObject.Find("Player").GetComponent<PlayerScript>().currentHP + "/" + GameManager.playerData.maxHP; 
        else textComponent.text = GameManager.playerData.currentHP + "/" + GameManager.playerData.maxHP;
    }
}
