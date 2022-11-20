using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EquipmentDisplayController : MonoBehaviour
{
    public GameObject equipmentCard;
    public List<CardData> cards;
    public List<GameObject> allArmors;
    public GameObject chestArmor;
    public GameObject feetArmor;

    public void Awake()
    {
        GenerateArmor();
    }
    public void UpdateDisplay()
    {
        ArmorSlot();
        foreach (GameObject armorObject in allArmors) {
            if (armorObject.GetComponent<CardDisplay>().cardData == GameManager.playerData.chestArmor || armorObject.GetComponent<CardDisplay>().cardData == GameManager.playerData.feetArmor)
            {
                armorObject.GetComponentInChildren<Image>().color = Color.gray;
                armorObject.GetComponent<EquipmentCard>().enabled = false;
            }
            else
            {
                armorObject.GetComponentInChildren<Image>().color = Color.white;
                armorObject.GetComponent<EquipmentCard>().enabled = true;
            }
        }
    }
    public void ArmorSlot()
    {
        if (GameManager.playerData.chestArmor != null)
        {
            foreach (Transform child in GameObject.Find("ArmorSlot").transform) Destroy(child.gameObject);
            equipmentCard.GetComponent<CardDisplay>().cardData = GameManager.playerData.chestArmor;
            chestArmor = Instantiate(equipmentCard, GameObject.Find("ArmorSlot").transform);
            chestArmor.transform.localScale = new Vector3(2f, 2f, 1f);
        }

        if (GameManager.playerData.feetArmor != null)
        {
            equipmentCard.GetComponent<CardDisplay>().cardData = GameManager.playerData.feetArmor;
            feetArmor = Instantiate(equipmentCard, GameObject.Find("ArmorSlot").transform);
            feetArmor.transform.localScale = new Vector3(2f, 2f, 1f);
        }
    }
    public void GenerateArmor()
    {
        ArmorSlot(); 

        foreach (CardData card in GameManager.playerData.inventory.Where(card => card.GetType() == typeof(Armor)).ToList())
        {
            Debug.Log("EQUIPMENT DISPLAY: " + card.name);
            var armor = (Armor)card;

            if (cards.Find(dupCard => dupCard.name == armor.name) != null) continue;

            if (GameManager.playerData.inventory.Find(upArmor => upArmor.name == armor.improvedArmor?.name) != null) continue;


            equipmentCard.GetComponent<CardDisplay>().cardData = card;
            var armorObject = Instantiate(equipmentCard, GameObject.Find("Content").transform);

            if (card == GameManager.playerData.chestArmor || card == GameManager.playerData.feetArmor)
            {
                armorObject.GetComponentInChildren<Image>().color = Color.gray;
                armorObject.GetComponent<EquipmentCard>().enabled = false;
            }

            allArmors.Add(armorObject);
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
