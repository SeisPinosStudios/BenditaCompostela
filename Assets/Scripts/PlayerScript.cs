using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Player variables
    public Weapon weapon;
    public int hp;
    public int totalSpeed;
    public int currentSpeed;
    public List<CardData> playerDeck = new List<CardData>();
    #endregion
}
