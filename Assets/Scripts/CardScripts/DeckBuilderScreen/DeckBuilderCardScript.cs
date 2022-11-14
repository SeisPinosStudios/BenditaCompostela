using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class DeckBuilderCardScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        var cardList = GameObject.Find("DeckBuilderDisplayController").GetComponent<DeckBuilderDisplay>().cards;
        var cardName = this.GetComponent<CardDisplay>().cardData.name;

        /* Adds the selected card to the player deck */
        GameManager.playerData.playerDeck.Add(gameObject.GetComponent<CardDisplay>().cardData);
        GameObject.Find("DeckBuilderDisplayController").GetComponent<DeckBuilderDisplay>().InsertCardInDeck(gameObject.GetComponent<CardDisplay>().cardData);

        /* Removes the cardData from the list of cards that
         * are in the card panel */
        cardList.Remove(gameObject.GetComponent<CardDisplay>().cardData);

        /* Check if there are no more cards of the same type in
         * the list of cards in player's inventory to destroy
         * the game object */
        var duplicatedCard = cardList.Find(dupCard => dupCard.name == cardName);
        if (duplicatedCard == null) Destroy(gameObject);
        else gameObject.GetComponent<CardDisplay>().cardData = duplicatedCard;
    }
}
