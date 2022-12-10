using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{
    public GameObject destroyingObject;
    public AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void DestroyObject()
    {
        audioManager.PlaySound("BEstandar");
        if (gameObject != null) Destroy(gameObject);
    }
}
