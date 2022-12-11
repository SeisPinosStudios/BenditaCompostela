using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AlteredEffectsSprites : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Sprite> sprites;
    public CardData.TAlteredEffects effect;
    public int value;
    public Transform infoPivot;
    public GameObject infoPrefab;
    GameObject info;

    public void Awake()
    {
        GetComponentInChildren<Image>().sprite = sprites[(int)effect];
        GetComponentInChildren<TextMeshProUGUI>().text = "x" + value;
    }

    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        infoPrefab.GetComponent<AlteredEffectInfo>().effect = effect;
        info = Instantiate(infoPrefab, infoPivot);
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        Destroy(info);
    }
}
