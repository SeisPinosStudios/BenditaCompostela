using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node 
{
    public int context;
    public List<string> previousNodeGoNames;
    public string currentNodeGoName;
    public bool isCompleted { get; set; }
}
