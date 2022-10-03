using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;
    public Text nameText;
    public Text descText;
    public Image artworkImage;
    public Text cost;

    public Transform parent;

    void Start()
    {
        nameText.text = card.name;
        descText.text = card.description;
        //artworkImage.sprite = card.artwork;
        cost.text = card.cost.ToString();

        parent = gameObject.GetComponentInParent<Transform>();
    }

    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        Debug.Log("Ha entrado en carta" + name);
        transform.localScale = new Vector3(2f, 2f, 2f);
        transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 100, -2.0f);
        transform.DetachChildren();
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 100, 0.0f);
        transform.parent = parent;
    }
}
