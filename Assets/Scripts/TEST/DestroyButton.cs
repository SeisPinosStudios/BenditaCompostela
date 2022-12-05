using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{
    public GameObject destroyingObject;

    public void DestroyObject()
    {
        if(gameObject != null) Destroy(gameObject);
    }
}
