using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlteredEffectInfo : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CardData.TAlteredEffects effect;

    public void Awake()
    {
        Description();
    }

    public void Description()
    {
        switch (effect)
        {
            case CardData.TAlteredEffects.BLEED:
                text.text = "<sprite index=0> - Daña 1 por carga al jugar una carta. Reduce curaciones 50%.";
                break;
            case CardData.TAlteredEffects.POISON:
                text.text = "<sprite index=1> - Daña 2 por cada turno con el efecto activo.";
                break;
            case CardData.TAlteredEffects.BURN:
                text.text = "<sprite index=2> - Daña 1 por carga. Daña 2 a partir de 5 cargas.";
                break;
            case CardData.TAlteredEffects.VULNERABLE:
                text.text = "<sprite index=3> - Aumenta el daño sufrido por ataques, escala con número de cargas.";
                break;
            case CardData.TAlteredEffects.GUARDED:
                text.text = "<sprite index=4> - Reduce el daño sufrido por ataques.";
                break;
            case CardData.TAlteredEffects.INVULNERABLE:
                text.text = "<sprite index=5> - Mitiga el daño por completo.";
                break;
            case CardData.TAlteredEffects.CONFUSED:
                text.text = "<sprite index=6> - Todas las cartas cuestan 1 más.";
                break;
            case CardData.TAlteredEffects.DISARMED:
                text.text = "<sprite index=7> - No puedes cambiar de arma.";
                break;
        }
    }
}
