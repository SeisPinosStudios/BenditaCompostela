using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlteredEffectTriggerTest : MonoBehaviour
{    
    private Animator CharacterAnController;

    private void Start()
    {
        CharacterAnController = gameObject.GetComponentInChildren<Animator>();
    }

    public void Attack() 
    {
        StartCoroutine(AnimationCancel("isAttacking"));        
    }
    public void Damaged()
    {
        StartCoroutine(AnimationCancel("isDamaged"));
    }
    public void Poisoned()
    {
        CharacterAnController.SetBool("isPoisoned", true);
    }
    public void OnFire()
    {
        CharacterAnController.SetBool("isOnFire", true);
    }
    public void Vulnerable() 
    {
        CharacterAnController.SetBool("isVulnerable", true);    
    }

    IEnumerator AnimationCancel(string AnBool) {
        CharacterAnController.SetBool(AnBool, false);
        yield return null;
        CharacterAnController.SetBool(AnBool, true);
        StartCoroutine(DelayedAnimation(AnBool, CharacterAnController.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator DelayedAnimation(string AnBool, float delay = 0) {
        yield return new WaitForSeconds(delay);
        CharacterAnController.SetBool(AnBool, false);
    }
}
