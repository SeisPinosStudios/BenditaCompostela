using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "Map/maps")]
[System.Serializable]
public class Map //: ScriptableObject
{
    public List<Node> mapNodeList;
    public string mapName;
    public bool isCompleted;
}
