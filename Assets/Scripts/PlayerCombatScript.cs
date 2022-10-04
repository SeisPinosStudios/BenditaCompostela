using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    #region Player variables
    public int weapon;
    public int hp;
    public int totalSpeed;
    public int currentSpeed;
    public List<CardData> playerDeck = new List<CardData>();
    #endregion
}
