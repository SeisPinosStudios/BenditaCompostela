using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class DeckBuilderCardScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        audioManager.PlaySound("RCartas");
        var card = gameObject.GetComponent<CardDisplay>().cardData;
        var cardList = GameObject.Find("DeckBuilderDisplayController").GetComponent<DeckBuilderDisplay>().cards;
        var cardName = this.GetComponent<CardDisplay>().cardData.name;

        if (GameManager.playerData.playerDeck.Count >= 20) return;

        if (GameManager.playerData.playerDeck.Where((card) => card.name == cardName).ToList().Count() >= 3) return;

        /* Adds the selected card to the player deck and removes it from the inventory */
        GameManager.playerData.inventory.Remove(card);
        GameManager.playerData.playerDeck.Add(card);
        GameObject.Find("DeckBuilderDisplayController").GetComponent<DeckBuilderDisplay>().InsertCardInDeck(card);

        /* Removes the cardData from the list of cards that
         * are in the card panel */
        cardList.Remove(card);

        /* Checks if there are no more cards of the same type in
         * the list of cards in player's inventory to destroy
         * the game object */
        var duplicatedCard = cardList.Find(dupCard => dupCard.name == cardName);
        if (duplicatedCard == null)
        {
            Destroy(gameObject);
        }
        else gameObject.GetComponent<CardDisplay>().cardData = duplicatedCard;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioManager.PlaySound("HCartas");
    }
}
