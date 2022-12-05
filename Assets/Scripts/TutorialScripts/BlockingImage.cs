using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockingImage : MonoBehaviour
{
    void Update()
    {
        if(TutorialManager.IsShowing()) GetComponent<Image>().enabled = true;
        else GetComponent<Image>().enabled = false;
    }
}
