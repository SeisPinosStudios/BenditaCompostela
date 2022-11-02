using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleSceneChangeButton : MonoBehaviour
{
    public string destination;

    public void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => SceneTransition(destination));
    }

    public void SceneTransition(string destination)
    {
        SceneManager.LoadScene(destination);
    }
}
