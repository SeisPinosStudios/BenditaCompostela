using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    private void Start()
    {
        updateCoinCounter(GameManager.playerData.coins.ToString());
    }
    public void updateCoinCounter(string newCoins) {
        coinText.text = newCoins;
    }
    public void Update()
    {
        updateCoinCounter(GameManager.playerData.coins.ToString());
    }
}
