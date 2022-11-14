using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => SetCurrentLevelAndTransition());
    }

    public void SetCurrentLevelAndTransition() {
        GameManager.currentLevelNodeGoName = gameObject.name;
        GameManager.mapNodeList.Find(n => n.currentNodeGoName == gameObject.name).isCompleted = true;
        GameObject.Find("Slide").GetComponent<Ruta_1>().ToShop();
    }

    
}
