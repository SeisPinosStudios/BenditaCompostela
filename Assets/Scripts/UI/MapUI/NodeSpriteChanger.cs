using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeSpriteChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite nodeSpriteHighlighted;
    [SerializeField] private Sprite nodeSprite;
    
    private Image currentNodeImage;
    private Color myColor;
    private Color defColor;
    private bool nodeSelectedBlink;
    private bool r = true;
    private bool completed = false;
    private void Awake()
    {
        currentNodeImage = gameObject.GetComponent<Image>();
        myColor.a = 2f;
        defColor = new Vector4(255,255,255,1);
    }

    private void Update()
    {
        if (nodeSelectedBlink)
        {
            if (r)
            {
                currentNodeImage.color -= myColor * Time.deltaTime;
                if (currentNodeImage.color.a <= 0) r = false;
            }
            else
            {
                currentNodeImage.color += myColor * Time.deltaTime;
                if (currentNodeImage.color.a >= 1) r = true;
            }
        }        
    }
    public void NodeCanBeSelected() {        
        nodeSelectedBlink = true;
    }

    public void NodeIsCompleted() {
        currentNodeImage.sprite = nodeSprite;
        currentNodeImage.color = defColor;
        completed = true;
        if (gameObject.GetComponent<Button>() != null)
        {
            gameObject.GetComponent<Button>().enabled = false;
        }        
        nodeSelectedBlink = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (nodeSpriteHighlighted == null || completed) return;
        currentNodeImage.color = defColor;
        nodeSelectedBlink = false;
        currentNodeImage.sprite = nodeSpriteHighlighted;           
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (nodeSpriteHighlighted == null || completed) return;
        currentNodeImage.sprite = nodeSprite;
        nodeSelectedBlink = true;
        
    }

    
}
