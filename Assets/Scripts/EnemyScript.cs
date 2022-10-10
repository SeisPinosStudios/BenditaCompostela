using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Entity
{
    public void OnTurnBegin()
    {
        Debug.Log("Turno del enemigo.");

        StartCoroutine(EnemyTurn());
        for(int i = 0; i < this.energy; i++)
        {
            Debug.Log("Entró al for");
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Se ha usado un movimiento.");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("No soy feliz.");
    }
}
