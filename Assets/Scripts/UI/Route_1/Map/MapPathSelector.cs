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

        foreach (Node node in map.FindAll(n => n.context == GameManager.gameProgressContext))
        {

            if (GameManager.gameProgressContext != 0)
            {
                if (node.previousNodes.Find(n => n == lastCompletedNode.nodeGo) != null)
                {
                    node.nodeGo.SetActive(true);
                    node.nodeGo.GetComponent<NodeSpriteChanger>().NodeCanBeSelected();
                }
            }
            else
            {
                node.nodeGo.SetActive(true);
                node.nodeGo.GetComponent<NodeSpriteChanger>().NodeCanBeSelected();
            }

        }
    }
    public void ActivatePreviousSelectedNodes()
    {
        for (int i = 0; i < GameManager.completedNodes.Count; i++)
        {
            map[GameManager.completedNodes[i]].nodeGo.SetActive(true);
            map[GameManager.completedNodes[i]].nodeGo.GetComponent<NodeSpriteChanger>().NodeIsCompleted();
        }
    }

    public void LoadMapNodes()
    {
        for (int i = 0; i < GameManager.completedNodes.Count; i++)
        {
            map[GameManager.completedNodes[i]].isCompleted = true;
        }
    }

    public void UpdateMap()
    {
        LoadMapNodes();
        ActivateNextNodes();
        ActivatePreviousSelectedNodes();
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

}