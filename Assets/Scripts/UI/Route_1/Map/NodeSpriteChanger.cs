using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite nodeSpriteHighlighted;
    [SerializeField] private Sprite nodeSprite;
    
    private Image currentNodeImage;
    private Color myColor;
    private bool nodeSelectedBlink;
    private bool r = true;
    private void Start()
    {
        currentNodeImage = gameObject.GetComponent<Image>();
        myColor.a = 2f;
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
        gameObject.GetComponent<Image>().sprite = nodeSpriteHighlighted;
        nodeSelectedBlink = true;
    }

    public void NodeIsNotActive() {
        gameObject.GetComponent<Image>().sprite = nodeSprite;
        nodeSelectedBlink = false;
    }

    public void NodeIsCompleted() {
        gameObject.GetComponent<Image>().sprite = nodeSpriteHighlighted;
        nodeSelectedBlink = true;
    }
}