using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public List<string> scenes;
    public List<TutorialObject> tutorials;
    public static TutorialObject tutorial;
    public static Dictionary<string, TutorialObject> tutorialDictionary = new Dictionary<string, TutorialObject>();
    static GameObject text;
    public GameObject textPrefabAux;
    static GameObject textPrefab;
    static int textIndex = 0;
    static bool showingTutorial;

    public void Awake()
    {
        if (scenes.Count != tutorials.Count) return;
        textPrefab = textPrefabAux;
        textPrefab.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        for(int i = 0; i < scenes.Count; i++) tutorialDictionary.Add(scenes[i], tutorials[i]);
    }

    public static void ShowTutorial(string tutorialName)
    {
        showingTutorial = true;
        if(text != null) Destroy(text);
        if (!tutorialDictionary.ContainsKey(tutorialName)) return;
        tutorial = tutorialDictionary[tutorialName];
        text = Instantiate(textPrefab, GameObject.Find("====CANVAS====").transform);
        text.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = tutorial.text[textIndex];
        text.transform.localPosition = new Vector3(tutorial.x[textIndex], tutorial.y[textIndex], 0);
    }

    public static void ProgressTutorial()
    {
        
        if (textIndex++ == tutorial.text.Count - 1) { Destroy(text); showingTutorial = false; }
        else
        {
            text.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = tutorial.text[textIndex];
            text.transform.localPosition = new Vector3(tutorial.x[textIndex], tutorial.y[textIndex], 0);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && showingTutorial) ProgressTutorial();
    }

    public static bool IsShowing()
    {
        return showingTutorial;
    }
}
