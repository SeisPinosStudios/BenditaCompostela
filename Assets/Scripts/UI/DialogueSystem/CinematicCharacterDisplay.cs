using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicCharacterDisplay : MonoBehaviour
{
    string dialogue;
    string characterName;
    string lastCharacter;
    public GameObject background;
    public GameObject characterPivotGo;
    int listIndex = 0;




    [SerializeField] private Image matiasImageField;
    [SerializeField] private Image marcosImageField;
    [SerializeField] private Image elviraImageField;

    [SerializeField] private List<Sprite> expressionsSprites;


    public enum Expressions{ 
        MAT_FELIZ, MAT_TRISTE_1, MAT_TRISTE_2, MAR_NORMAL, MAR_TRISTE, ELV_TRISTE, ELV_ENFADADA
    }
    List<Expressions> expressionsList = new List<Expressions>() 
    {
        Expressions.MAT_FELIZ,Expressions.ELV_TRISTE,Expressions.MAR_NORMAL,Expressions.MAT_FELIZ,Expressions.MAR_NORMAL,Expressions.ELV_TRISTE,Expressions.MAR_TRISTE,Expressions.MAT_TRISTE_1,
        Expressions.ELV_TRISTE,Expressions.MAT_TRISTE_1, Expressions.MAR_TRISTE,Expressions.MAT_TRISTE_1,Expressions.MAR_TRISTE,Expressions.MAT_TRISTE_2, Expressions.MAR_TRISTE,Expressions.ELV_TRISTE,Expressions.MAT_TRISTE_2,Expressions.ELV_ENFADADA,Expressions.MAR_TRISTE,
        Expressions.MAT_TRISTE_2,Expressions.MAR_NORMAL,Expressions.ELV_ENFADADA
    };

    public void CharacterSelector(string dialogueUIText) {
        dialogue = dialogueUIText;
        lastCharacter = characterName;
        CharacterReader();

        if (characterName == lastCharacter) return;

        switch (characterName) {
            case "MATÍAS":
                matiasImageField.sprite = GetSprite(expressionsList[listIndex]);
                matiasImageField.color = ConvertColorFormat(new Vector3(255,255,255)); 
                marcosImageField.color = ConvertColorFormat(new Vector3(128, 128, 128));
                elviraImageField.color = ConvertColorFormat(new Vector3(128, 128, 128));
                listIndex++;
                break;
            case "MARCOS":
                marcosImageField.sprite = GetSprite(expressionsList[listIndex]);
                matiasImageField.color = ConvertColorFormat(new Vector3(128, 128, 128));
                marcosImageField.color = ConvertColorFormat(new Vector3(255, 255, 255));
                elviraImageField.color = ConvertColorFormat(new Vector3(128, 128, 128));
                listIndex++;
                break;
            case "ELVIRA":                
                elviraImageField.sprite = GetSprite(expressionsList[listIndex]);
                matiasImageField.color = ConvertColorFormat(new Vector3(128, 128, 128));
                marcosImageField.color = ConvertColorFormat(new Vector3(128, 128, 128));
                elviraImageField.color = ConvertColorFormat(new Vector3(255, 255, 255));
                listIndex++;
                break;
        }
    }

    public Color ConvertColorFormat(Vector3 newColor) {
        newColor /= 255;
        return new Color(newColor.x, newColor.y, newColor.z);
    }

    public Sprite GetSprite(Expressions expression) {
        switch (expression) {
            case Expressions.MAT_FELIZ:     return expressionsSprites[0];                
            case Expressions.MAT_TRISTE_1:  return expressionsSprites[1];                
            case Expressions.MAT_TRISTE_2:  return expressionsSprites[2];                
            case Expressions.ELV_TRISTE:    return expressionsSprites[3];                
            case Expressions.ELV_ENFADADA:  return expressionsSprites[4];                
            case Expressions.MAR_NORMAL:    return expressionsSprites[5];                
            case Expressions.MAR_TRISTE:    return expressionsSprites[6];                
            default:                        return null;                
        }
    }



    public void CharacterReader() {
        int index = 0;
        string aux = null;
        bool nameComplete = false;
        while (!nameComplete)
        {
            if (char.IsUpper(dialogue[index]))
            {
                aux += dialogue[index];               
                index++;
            }
            else nameComplete = true;            
        }
        characterName = aux;
        Debug.Log(characterName);
    }


    public void EneableBackground()
    {
        background.SetActive(true);
        characterPivotGo.SetActive(true);
    }

}
