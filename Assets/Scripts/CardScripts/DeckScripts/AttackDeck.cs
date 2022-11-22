using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDeck : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;
    public GameObject card;
    public List<Sprite> images;
    public Image image;

    public void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => StartDrawCoroutine(1));
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
        Debug.Log("DRAW CARD");
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("DrawCard");
    }
    IEnumerator DrawCardCoroutine(int drawnCards)
    {
        Debug.Log("DRAWN CARDS: " + drawnCards);
        for (int j = 0; j < drawnCards; j++)
        {
            Debug.Log("DRAW COROUTINE" + j);
            DrawFreeCard();
            yield return new WaitForSeconds(0.2f);
        }

        foreach (Transform card in hand.transform)
        {
            card.GetComponent<CardInspection>().canInspect = true;
        }
    }
    public void StartDrawCoroutine(int drawnCards)
    {
        StartCoroutine(DrawCardCoroutine(drawnCards));
    }
    public void Update()
    {
        if (player.GetComponent<PlayerScript>().weapon == null) gameObject.GetComponent<Button>().enabled = false;

        if (GameObject.Find("Player").GetComponent<PlayerScript>().weapon == null) image.sprite = images[5];
        else image.sprite = images[GameObject.Find("Player").GetComponent<PlayerScript>().weapon.weaponId];
    }
}
