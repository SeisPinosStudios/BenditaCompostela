using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckBuilderSlotScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        var cardList = GameObject.Find("DeckBuilderDisplayController").GetComponent<DeckBuilderDisplay>().cards;

        /* Removes the card from the player deck */
        GameManager.playerData.playerDeck.Remove(gameObject.GetComponent<DeckSlotDisplay>().cardData);
        GameObject.Find("DeckBuilderDisplayController").GetComponent<DeckBuilderDisplay>().RemoveCardFromDeck(gameObject.GetComponent<DeckSlotDisplay>().cardData);

        /* Adds the card to the list of cards in the Display Panel */
        cardList.Add(gameObject.GetComponent<DeckSlotDisplay>().cardData);

        /* Deletes the selected slot */
        Destroy(gameObject);
    }
}
