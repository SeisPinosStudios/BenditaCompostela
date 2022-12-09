using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassiveDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Sprite> sprites;
    public Enemy.Passive passive;
    public GameObject passivePrefab;
    GameObject info;

    public void Awake()
    {
        GetComponentInChildren<Image>().sprite = sprites[(int)passive];
    }

    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        passivePrefab.GetComponent<PassiveInfo>().passive = passive;
        info = Instantiate(passivePrefab, transform.GetChild(1).transform);
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        Destroy(info);
    }
}
