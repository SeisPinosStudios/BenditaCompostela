using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    bool open = false;
    public GameObject settingsPrefab;
    public Transform pivot;
    public GameObject settingsScreen;
    public AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Settings()
    {
        SettingsSound();
        Debug.Log("Settings");
        if (settingsScreen == null) OpenSettings();
        else settingsScreen.gameObject.SetActive(true);
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
    public void SettingsSound() {
        audioManager.PlaySound("BEstandar");
    }
}
