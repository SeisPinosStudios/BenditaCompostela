using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterAnimationEventSystem : MonoBehaviour
{

    public Image chest;
    public Image feet;
    public Image weapon;
    public Image alteredEffectDisplay;

    public int chestIndex;
    public int feetIndex;
    public int weaponIndex;
    public int slashIndex;

    public List<SpriteSheet> chestList;
    public List<Sprite> feetList;
    public List<SpriteSheet> weaponList;
    public List<SpriteSheet> slashList;
    public List<SpriteSheet> bleedSlashList;

    public SpriteSheet poisonSpriteSheet;
    public SpriteSheet vulnerableSpriteSheet;
    public SpriteSheet burnSpriteSheet;
    public SpriteSheet healingSpriteSheet;

    PlayerScript player;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene") player = GameObject.Find("Player").GetComponent<PlayerScript>();
        else weaponIndex = 5;
        chestIndex = GameManager.playerData.chestArmor.armorId;
        feetIndex = GameManager.playerData.feetArmor.armorId;
        //weaponIndex = GameManager.playerData.;


    }

    private void Update()
    {
        if (player != null) weaponIndex = player.weapon == null ? 5 : player.weapon.weaponId;
    }
    public void SetSprite(int index) {
        chest.sprite = chestList[chestIndex].spriteList[index];
        feet.overrideSprite = feetList[feetIndex];
        weapon.overrideSprite = weaponList[weaponIndex].spriteList[index];
    }
    public void SetRandomSlash(int index) {
        
        alteredEffectDisplay.overrideSprite = slashList[slashIndex].spriteList[index];
    }

    public void SetRandomBleedSlash(int index)
    {    
        alteredEffectDisplay.overrideSprite = bleedSlashList[slashIndex].spriteList[index];
    }

    public void GenerateSlashIndex() {
        
        slashIndex = Random.Range(0,7);
    }
    public void SetPoisonAlteredEffect(int index) {
        alteredEffectDisplay.overrideSprite = poisonSpriteSheet.spriteList[index];
    }
    public void SetVulnerableAlteredEffect(int index)
    {
        alteredEffectDisplay.overrideSprite = vulnerableSpriteSheet.spriteList[index];
    }
    public void SetBurnAlteredEffect(int index)
    {
        alteredEffectDisplay.overrideSprite = burnSpriteSheet.spriteList[index];
    }
    public void SetHealingAlteredEffect(int index)
    {
        alteredEffectDisplay.overrideSprite = healingSpriteSheet.spriteList[index];
    }
}
