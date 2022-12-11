using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialButton : MonoBehaviour
{
    public string tutorial;
    public void StartTutorial()
    {
        TutorialManager.ShowTutorial(tutorial);
    }
}
