using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    bool open = false;
    public GameObject settingsPrefab;
    public Transform pivot;
    GameObject settingsScreen;
    public void Awake()
    {
    }

    public void Settings()
    {
        if (settingsScreen != null) settingsScreen.gameObject.SetActive(true);
        else OpenSettings();
    }

    public void OpenSettings()
    {
        open = !open;
        settingsScreen = Instantiate(settingsPrefab, pivot);
    }

    public void CloseSettings()
    {
        open = !open;
        Destroy(settingsScreen);
    }
}
