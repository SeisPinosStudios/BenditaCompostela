using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDeck : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;
    public GameObject card;

    public void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(DrawCard);
    }
    public void Update()
    {
        if(player.GetComponent<PlayerScript>().weapon == null) gameObject.GetComponent<Button>().enabled = false;
    }
    public void DrawCard()
    {
        Weapon weapon = player.GetComponent<PlayerScript>().weapon;
        card.GetComponent<CardDisplay>().cardData = weapon.attackList[Random.Range(0, weapon.attackList.Count)];
        if (player.GetComponent<PlayerScript>().ConsumeEnergy(1)) Instantiate(card, hand.transform);

        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("DrawCard");
    }
    public void DrawFreeCard()
    {
        Weapon weapon = player.GetComponent<PlayerScript>().weapon;
        card.GetComponent<CardDisplay>().cardData = weapon.attackList[Random.Range(0, weapon.attackList.Count)];
        Instantiate(card, hand.transform);
    }
    public IEnumerator DrawCardCorroutine(int drawnCards)
    {
        for (int j = 0; j < drawnCards; j++)
        {
            DrawFreeCard();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
