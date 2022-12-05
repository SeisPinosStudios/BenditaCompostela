using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New tutorial", menuName = "BenditaCompostela/Tutorial")]
public class TutorialObject : ScriptableObject
{
    [TextArea(5,20)]
    public List<string> text;
    public List<float> x;
    public List<float> y;
}
