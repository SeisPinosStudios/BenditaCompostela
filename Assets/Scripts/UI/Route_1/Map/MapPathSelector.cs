using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPathSelector : MonoBehaviour
{
    [SerializeField] private List<Node> map;

    private void Start()
    {        
        UpdateMap();
        
    }

    public void ActivateNextNodes()
    {
        Node lastCompletedNode = null;
        if (GameManager.gameProgressContext != 0)
        {
            lastCompletedNode = map.Find(n => n.context == GameManager.gameProgressContext - 1 && n.isCompleted);
        }
        foreach (Node node in map.FindAll(n => n.context == GameManager.gameProgressContext ))
        {          
            if (GameManager.gameProgressContext != 0)
            {
<<<<<<< Updated upstream
                if (node.previousNodes.Find(n => n == lastCompletedNode.nodeGo) != null)
                {
=======
                var prevNodeStruct = node.previousNodes.Find(n => n.previousNodeGo == lastCompletedNode.nodeGo);                
                if (prevNodeStruct.previousNodeGo == lastCompletedNode.nodeGo)
                {                    
>>>>>>> Stashed changes
                    node.nodeGo.SetActive(true);
                    prevNodeStruct.previousNodePathGo.SetActive(true);

                    node.nodeGo.GetComponent<NodeSpriteChanger>().NodeCanBeSelected();
                    prevNodeStruct.previousNodePathGo.GetComponent<NodeSpriteChanger>().NodeCanBeSelected();

                    foreach (PreviousNode item in node.previousNodes.FindAll(n => n.previousNodePathGo != prevNodeStruct.previousNodePathGo))
                    {
                        item.previousNodePathGo.SetActive(false);
                    }
                    
                }
                else {                    
                    node.nodeGo.SetActive(false);
                    foreach (PreviousNode prevNode in node.previousNodes) prevNode.previousNodePathGo.SetActive(false);
                }
            }
            else
            {                
                node.nodeGo.SetActive(true);
                node.nodeGo.GetComponent<NodeSpriteChanger>().NodeCanBeSelected();

                foreach (PreviousNode prevNodeStruct in node.previousNodes) {
                    prevNodeStruct.previousNodePathGo.SetActive(true);
                    prevNodeStruct.previousNodePathGo.GetComponent<NodeSpriteChanger>().NodeCanBeSelected();
                }
                
            }

        }
    }
    public void UpdateMap()
    {
<<<<<<< Updated upstream
        for (int i = 0; i < GameManager.completedNodes.Count; i++)
        {
            map[GameManager.completedNodes[i]].nodeGo.SetActive(true);
            map[GameManager.completedNodes[i]].nodeGo.GetComponent<NodeSpriteChanger>().NodeIsCompleted();
        }
=======
        LoadMapNodes();
        ActivatePreviousSelectedNodes();
        ActivateNextNodes();
>>>>>>> Stashed changes
    }
    public void LoadMapNodes()
    {
        for (int i = 0; i < GameManager.completedNodes.Count; i++)
        {
            map[GameManager.completedNodes[i]].isCompleted = true;
        }
    }

    public void ActivatePreviousSelectedNodes()
    {
        DeactivatePreviosNotSelecteNodes();
        for (int i = 0; i < GameManager.completedNodes.Count; i++)
        {            
            map[GameManager.completedNodes[i]].nodeGo.SetActive(true);

            foreach (PreviousNode prevNodeStruct in map[GameManager.completedNodes[i]].previousNodes)
            {                
                if (prevNodeStruct.previousNodeGo == null)
                {
                    prevNodeStruct.previousNodePathGo.SetActive(true);
                    prevNodeStruct.previousNodePathGo.GetComponent<NodeSpriteChanger>().NodeIsCompleted();
                }
                else {                                     
                    if (map.Find(n => n.nodeGo == prevNodeStruct.previousNodeGo).isCompleted)
                    {                        
                        prevNodeStruct.previousNodePathGo.SetActive(true);
                        prevNodeStruct.previousNodePathGo.GetComponent<NodeSpriteChanger>().NodeIsCompleted();
                    }
                }
            }
            map[GameManager.completedNodes[i]].nodeGo.GetComponent<NodeSpriteChanger>().NodeIsCompleted();            
        }
    }
    public void DeactivatePreviosNotSelecteNodes()
    {
        foreach (Node node in map)
        {
             node.nodeGo.SetActive(false);
             foreach (PreviousNode prevNode in node.previousNodes) prevNode.previousNodePathGo.SetActive(false);
        }
    }
    

    public int GetGoIndex(GameObject go)
    {
        if (map.Find(n => n.nodeGo == go) != null)
        {
            return map.Find(n => n.nodeGo == go).index;
        }
        else
        {
            return -1;
        }

    }
<<<<<<< Updated upstream

=======
   
>>>>>>> Stashed changes
}