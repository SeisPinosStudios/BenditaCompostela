using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EncounterButton : MonoBehaviour
{

    public Enemy enemy;
    public int encounterId;
    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => SetCurrentLevelAndTransition());
    }

    public void SetCurrentLevelAndTransition()
    {
        GameManager.currentLevelNodeGoName = gameObject.name;
        GameManager.mapNodeList.Find(n => n.currentNodeGoName == gameObject.name).isCompleted = true;
        GameObject.Find("Slide").GetComponent<Ruta_1>().ToEncounter(encounterId);
    }
    public void ToBattleScene(Enemy enemy)
    {
        GameManager.nextEnemy = enemy;
        GameManager.currentLevelNodeGoName = gameObject.name;
        SceneManager.LoadScene("BattleScene");
    }
}
