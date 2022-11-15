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
    private Color defColor;
    private bool nodeSelectedBlink;
    private bool r = true;
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
        currentNodeImage.sprite = nodeSpriteHighlighted;
        nodeSelectedBlink = true;
    }

    public void NodeIsNotActive() {
        currentNodeImage.sprite = nodeSprite;
        nodeSelectedBlink = false;
    }

    public void NodeIsCompleted() {
        currentNodeImage.sprite = nodeSpriteHighlighted;
        currentNodeImage.color = defColor;
        gameObject.GetComponent<Button>().enabled = false;
        nodeSelectedBlink = false;
    }
}
