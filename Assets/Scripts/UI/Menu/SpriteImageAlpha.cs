using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class SpriteImageAlpha : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    private void Start()
    {
        gameObject.GetComponent<Image>().color = new Vector4(0,0,0,0);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Vector4(255, 255, 255, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Vector4(0, 0, 0, 0);
    }
}
