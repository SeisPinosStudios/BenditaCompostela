using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SimpleSceneChangeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string destination;
    public Image buttonImage;
    public List<Sprite> sprites;

    public void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => SceneTransition(destination));
    }

    public void SceneTransition(string destination)
    {
        if (destination == "Back") {
            SceneManager.LoadScene(GameManager.ActualRoute);
            return;
        }

        GameManager.ActualRoute = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(destination);
    }

    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        if (buttonImage == null) return;
        buttonImage.sprite = sprites[1];
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        if (buttonImage == null) return;
        buttonImage.sprite = sprites[0];
    }
}
