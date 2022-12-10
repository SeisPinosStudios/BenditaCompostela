using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DeckBuilderHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Animator anController;
    void Start()
    {
        anController = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anController.SetBool("onHover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anController.SetBool("onHover", false);
    }

}
