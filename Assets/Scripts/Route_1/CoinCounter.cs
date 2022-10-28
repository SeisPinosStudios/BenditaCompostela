using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private PlayerScript player;
    private void Start()
    {
        updateCoinCounter(player.getPlayerCoins().ToString());        
    }

    public void updateCoinCounter(string newCoins) {
        coinText.text = newCoins;
    }


}
