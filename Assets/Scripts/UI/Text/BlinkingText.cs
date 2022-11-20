using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TMP_Text text;
    private bool r;
    void Start()
    {
        
        text = gameObject.GetComponent<TMP_Text>();
        text.alpha = 0;
        r = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (r)
        {
            text.alpha -= 1 * Time.deltaTime;
            if (text.alpha <= 0) r = false;            
        }
        else {
            text.alpha += 1 * Time.deltaTime;
            if (text.alpha >= 1) r = true;
        }
    }
    }   

