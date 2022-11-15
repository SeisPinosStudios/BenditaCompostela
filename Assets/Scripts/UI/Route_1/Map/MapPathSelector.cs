using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPathSelector : MonoBehaviour
{    
    private void Start()
    {        
        ActivateNextNodes();
        ActivatePreviousSelectedNodes();
    }
    public void ActivateNextNodes() {        
        if (GameManager.gameProgressContext == 0)
        {
            GameManager.GetCurrentLevelNode().SetActive(true);
            GameManager.GetCurrentLevelNode().GetComponent<NodeSpriteChanger>().NodeCanBeSelected();
            return;
        }
        Node prevCompleteNode = GameManager.mapNodeList.Find(n => n.context == GameManager.gameProgressContext - 1 && n.isCompleted);
        foreach (Node node in GameManager.mapNodeList.FindAll(n => n.context == GameManager.gameProgressContext))
        {
            /*
            #region DEBUG
            var aux = GameManager.mapNodeList.FindAll(n => n.context == GameManager.gameProgressContext);
            for (int i = 0; i < aux.Count; i++)
            {
                Debug.Log("Node with context = " + GameManager.gameProgressContext + " : " + aux[i].currentNodeGoName);
            }
            for (int i = 0; i < node.previousNodeGoNames.Count; i++)
            {
                Debug.Log("Previous nodes [" + i + "]: " + node.previousNodeGoNames[i]);
            }
            Debug.Log("Prev Completed Node Name: " + prevCompleteNode.currentNodeGoName);

            #endregion
            */
            if (node.previousNodeGoNames.Find(n => n == prevCompleteNode.currentNodeGoName) != null)
            {
                //Debug.Log("Node Activated : " + node.currentNodeGoName);
                GameManager.GetAnyLevelNode(node.currentNodeGoName).SetActive(true);
                GameManager.GetAnyLevelNode(node.currentNodeGoName).GetComponent<NodeSpriteChanger>().NodeCanBeSelected();                
            }            
        }        
    }
    public void ActivatePreviousSelectedNodes() {
        foreach (Node node in GameManager.mapNodeList.FindAll(n => n.isCompleted == true))
        {
            //Debug.Log("ActivatePreviousSelectedNodes");
            GameManager.GetAnyLevelNode(node.currentNodeGoName).SetActive(true);
            GameManager.GetAnyLevelNode(node.currentNodeGoName).GetComponent<NodeSpriteChanger>().NodeIsCompleted();
        }
        foreach (Node node in GameManager.mapNodeList.FindAll(n => !n.isCompleted && n.context != GameManager.gameProgressContext)) {
            GameManager.GetAnyLevelNode(node.currentNodeGoName).SetActive(false);
        }
    }

    public void UpdateMap() {
        //Debug.Log("UPDAATE");
        ActivateNextNodes();
        ActivatePreviousSelectedNodes();
    }

}
