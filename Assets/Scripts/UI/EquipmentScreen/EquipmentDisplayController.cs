using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EquipmentDisplayController : MonoBehaviour
{
    public GameObject equipmentCard;
    public List<CardData> cards;

    public void Awake()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        ClearDisplay();

        GameObject cardObject = null;

        if (GameManager.playerData.chestArmor != null)
        {
            equipmentCard.GetComponent<CardDisplay>().cardData = GameManager.playerData.chestArmor;
            Instantiate(equipmentCard, GameObject.Find("ArmorSlot").transform).transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        }

        if (GameManager.playerData.feetArmor != null)
        {
            equipmentCard.GetComponent<CardDisplay>().cardData = GameManager.playerData.feetArmor;
            Instantiate(equipmentCard, GameObject.Find("ArmorSlot").transform).transform.localScale = new Vector3(2.0f, 2.0f, 1.0f); ;
        }

        foreach (CardData card in GameManager.playerData.inventory.Where(card => card.GetType() == typeof(Armor)))
        {
            equipmentCard.GetComponent<CardDisplay>().cardData = card;
            if (cards.Find(dupCard => dupCard.name == card.name) == null)
            {
                cardObject = Instantiate(equipmentCard, GameObject.Find("Content").transform);

                if (card == GameManager.playerData.chestArmor || card == GameManager.playerData.feetArmor)
                {
                    cardObject.GetComponentInChildren<Image>().color = Color.gray;
                    cardObject.GetComponent<EquipmentCard>().enabled = false;
                }
            }
            cards.Add(card);
        }
    }

    public void ClearDisplay()
    {
        foreach (Transform child in GameObject.Find("Content").transform) Destroy(child.gameObject);

        foreach (Transform child in GameObject.Find("ArmorSlot").transform) Destroy(child.gameObject);

        cards.Clear();
    }
}
