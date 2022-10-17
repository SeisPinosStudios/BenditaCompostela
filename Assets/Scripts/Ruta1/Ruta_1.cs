using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ruta_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void changeLevel(int lvl) {
        SceneManager.LoadScene(lvl);
    }
}
