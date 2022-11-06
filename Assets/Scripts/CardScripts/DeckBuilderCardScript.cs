using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class DeckBuilderCardScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        /* Removes the cardData from the list of cards that
         * are in the card panel */
        GameObject.Find("CardGenerator").GetComponent<CardGenerator>().cards.Remove(gameObject.GetComponent<CardDisplay>().cardData);

        /* Adds the selected card to the player deck */
        GameManager.playerData.playerDeck.Add(Instantiate(gameObject.GetComponent<CardDisplay>().cardData));

        /* Check if there are no more cards of the same type in
         * the list of cards in player's inventory to destroy
         * the game object */
        if (! GameObject.Find("CardGenerator").GetComponent<CardGenerator>().cards.Contains(gameObject.GetComponent<CardDisplay>().cardData)) Destroy(gameObject);
    }
}
