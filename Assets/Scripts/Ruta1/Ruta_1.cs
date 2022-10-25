using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ruta_1 : MonoBehaviour
{
    Animator slideAn;
    public GameObject fadeGo;
    Fade fade;
    void Start()
    {
        slideAn = gameObject.GetComponent<Animator>();
        fade = fadeGo.GetComponent<Fade>();
    }

    public void ruta1ToShop() {
        slideAn.SetBool("isShop", true);
    }

    public void shopToRuta1() {
        slideAn.SetBool("isShop", false);
    }
    void changeLevel(int lvl) {
        SceneManager.LoadScene(lvl);
    }
}
