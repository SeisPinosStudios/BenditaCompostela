using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoreButton : MonoBehaviour
{
    public int loreId;
    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => SetCurrentLevelAndTransition());
    }

    public void SetCurrentLevelAndTransition()
    {
        GameManager.currentLevelNodeGoName = gameObject.name;
        GameManager.mapNodeList.Find(n => n.currentNodeGoName == gameObject.name).isCompleted = true;
        GameObject.Find("Slide").GetComponent<Ruta_1>().ToLore(loreId);
    }
}
