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
        if (CharacterAnController == null) return;
        StartCoroutine(AnimationCancel("isAttacking"));        
    }
    public void Damaged()
    {
        if (CharacterAnController == null) return;
        StartCoroutine(AnimationCancel("isDamaged"));
    }
    public void Poisoned()
    {
        if (CharacterAnController == null) return;
        CharacterAnController.SetBool("isPoisoned", true);
    }
    public void OnFire()
    {
        if (CharacterAnController == null) return;
        CharacterAnController.SetBool("isOnFire", true);
    }
    public void Vulnerable() 
    {
        if (CharacterAnController == null) return;
        CharacterAnController.SetBool("isVulnerable", true);    
    }

    public void Bleed() {
        if (CharacterAnController == null) return;
        StartCoroutine(AnimationCancel("isBleeding"));
    }

    public void Heal() {
        if (CharacterAnController == null) return;
        CharacterAnController.SetBool("isHealing", true);
    }

    public float AnimationDelay() {
        return CharacterAnController.GetCurrentAnimatorStateInfo(0).length;
    }
    IEnumerator AnimationCancel(string AnBool) {
        CharacterAnController.SetBool(AnBool, false);
        yield return null;
        CharacterAnController.SetInteger("isDamagedIndex", Random.Range(0,7));
        CharacterAnController.SetBool(AnBool, true);
        StartCoroutine(DelayedAnimation(AnBool, AnimationDelay()));
    }

    IEnumerator DelayedAnimation(string AnBool, float delay = 0) {
        yield return new WaitForSeconds(delay);
        CharacterAnController.SetBool(AnBool, false);
    }
}
