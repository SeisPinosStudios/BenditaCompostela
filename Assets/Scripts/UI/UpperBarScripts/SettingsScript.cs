using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    bool open = false;
    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Settings);
    }

    public void Settings()
    {
        if (!open) OpenSettings();
        else CloseSettings();
    }

    public void OpenSettings()
    {
        open = !open;
    }

    public void CloseSettings()
    {
        open = !open;
    }
}
