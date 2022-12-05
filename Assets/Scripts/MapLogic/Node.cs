using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node 
{
    public int index;
    public int context;
    public List<PreviousNode> previousNodes;
    public GameObject nodeGo;
    public bool isCompleted;
}
