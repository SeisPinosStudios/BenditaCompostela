using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteredEffectTriggerTest : MonoBehaviour
{
    public Animator AlteredEffectDisplay;
    public Animator CharacterAnController;       
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AlteredEffectDisplay.SetBool("isPoisoned", true);
            CharacterAnController.SetBool("isPoisoned", true);
        }
        if (Input.GetKeyUp(KeyCode.P)) {
            AlteredEffectDisplay.SetBool("isPoisoned", false);
            CharacterAnController.SetBool("isPoisoned", false);
        }
        
    }
}
