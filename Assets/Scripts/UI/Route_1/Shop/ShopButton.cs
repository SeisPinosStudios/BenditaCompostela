using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    private MapPathSelector mapController;
    
    private void OnEnable()
    {
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapPathSelector>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => SetCurrentLevelAndTransition());
    }

    public void SetCurrentLevelAndTransition() {
        GameManager.currentNode = mapController.GetGoIndex(gameObject);
        GameObject.Find("Slide").GetComponent<RouteNavigator>().ToShop();
    }
}
