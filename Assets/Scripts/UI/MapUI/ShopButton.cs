using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    private MapPathSelector mapController;
    public AudioManager audioManager;

    private void OnEnable()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapPathSelector>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => SetCurrentLevelAndTransition());

    }

    public void SetCurrentLevelAndTransition() {
        audioManager.PlaySound("Tienda");
        GameManager.currentNode = mapController.GetGoIndex(gameObject);
        GameObject.Find("Slide").GetComponent<RouteNavigator>().ToShop();
    }
}
