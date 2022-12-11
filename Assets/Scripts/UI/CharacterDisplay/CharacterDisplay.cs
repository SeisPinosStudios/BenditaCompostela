using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterDisplay : MonoBehaviour
{
    public List<Sprite> chestArmor;
    public List<Sprite> feetArmor;
    public List<Sprite> weapon;

    public Image chestImage;
    public Image feetImage;
    public Image weaponImage;

    public PlayerScript player;
    public GameObject cardPrefab;
    public GameObject cardDisplay;

    public void Awake()
    {

    }
    private void Update()
    {
        chestImage.sprite = chestArmor[GameManager.playerData.chestArmor.armorId];
        feetImage.sprite = feetArmor[GameManager.playerData.feetArmor.armorId];

        if (SceneManager.GetActiveScene().name != "BattleScene") return;

        if (player.weapon == null) weaponImage.sprite = weapon[5];
        else weaponImage.sprite = weapon[player.GetComponent<PlayerScript>().weapon.weaponId];
    }
}

