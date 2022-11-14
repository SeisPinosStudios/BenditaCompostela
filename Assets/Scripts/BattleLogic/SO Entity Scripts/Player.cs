using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New player configuration", menuName = "BenditaCompostela/New player")]
public class Player : ScriptableObject
{
    /* Statistic variables for 
     * the player entity */
    public int HP;
    public int energy;
    public int coins;

    /* Player deck and inventory
     * this two lists control the 
     * cards a player has equiped
     * and avalible */
    public List<CardData> playerDeck;
    public List<CardData> inventory;
    public Armor chestArmor;
    public Armor feetArmor;

    public bool CoinDecrease(int cost)
    {
        if (cost > coins) return false;
        coins -= cost;
        return true;
    }

    public Player copy()
    {
        Player player = CreateInstance<Player>();
        player.HP = HP;
        player.energy = energy;
        player.coins = coins;
        player.playerDeck = playerDeck.Select((card) => Instantiate(card)).ToList();
        player.inventory = inventory.Select((card) => Instantiate(card)).ToList();
        player.chestArmor = Instantiate(chestArmor);
        player.feetArmor = Instantiate(feetArmor);
        return player;
    }
}
