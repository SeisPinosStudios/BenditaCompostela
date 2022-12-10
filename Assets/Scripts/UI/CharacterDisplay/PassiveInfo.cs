using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PassiveInfo : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Enemy.Passive passive;

    public void Awake()
    {
        Description();
    }

    public void Description()
    {
        switch (passive)
        {
            case Enemy.Passive.SIERPE:
                text.text = "<sprite index=8>: las duras escamas que rodean el cuerpo de la sierpe hacen que sufra 2 puntos menos de daño con cada ataque.";
                break;
            case Enemy.Passive.HERNAN:
                text.text = "<sprite index=9>: la habilidad de Hernán con la espada hace que sus ataques vayan bien dirijidos y certeros, provocando x1 de sangrado con cada ataque";
                break;
            case Enemy.Passive.TRASGU:
                text.text = "<sprite index=10>: el trasgu es una criatura perversa y escurridiza, cada vez que laces una carta tienes un 10% de posibilidades de fallarla.";
                break;
            case Enemy.Passive.SANTIAGO:
                text.text = "<sprite index=11>: Matías sufre +2 de daño con todos los efectos de estado.";
                break;
            case Enemy.Passive.SANTIAGO_2:
                text.text = "<sprite index=12>: Santiago recibe -3 de daño con todos los efectos de estado que sufra.";
                break;
            case Enemy.Passive.rVULNERABLE:
                text.text = "<sprite index=13>: este enemigo es inmune a sufrir <sprite index=3>.";
                break;
            case Enemy.Passive.rGUARDED:
                text.text = "<sprite index=14>: no podrás ponerte cargas de En Guardia contra este <sprite index=4>.";
                break;
            case Enemy.Passive.rPOISON:
                text.text = "<sprite index=15>: este enemigo es inmune a sufrir daño por <sprite index=1>.";
                break;
            case Enemy.Passive.rBURN:
                text.text = "<sprite index=16>: este enemigo es inmune a sufrir daño por <sprite index=2>.";
                break;
            case Enemy.Passive.rBLEED:
                text.text = "<sprite index=17>: este enemigo es inmune a sufrir daño por <sprite index=0>.";
                break;
        }
    }
}
