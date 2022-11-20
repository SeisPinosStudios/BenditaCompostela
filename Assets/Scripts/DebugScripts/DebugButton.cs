using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(DebugSound);
    }

    public void DebugSound()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("sonido");
    }
}
