using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckBuilderSlotScript : MonoBehaviour, IPointerClickHandler
{
    public AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        audioManager.PlaySound("HCartas");
        var card = gameObject.GetComponent<DeckSlotDisplay>().cardData;
        var cardList = GameObject.Find("DeckBuilderDisplayController").GetComponent<DeckBuilderDisplay>().cards;

        /* Removes the card from the player deck */
        GameManager.playerData.inventory.Add(card);
        GameManager.playerData.playerDeck.Remove(card);
        GameObject.Find("DeckBuilderDisplayController").GetComponent<DeckBuilderDisplay>().RemoveCardFromDeck(card);

        /* Adds the card to the list of cards in the Display Panel */
        cardList.Add(card);

        /* Deletes the selected slot */
        Destroy(gameObject);
    }
}
