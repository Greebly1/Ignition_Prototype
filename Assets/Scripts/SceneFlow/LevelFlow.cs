using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject/LevelFlow")]
public class LevelFlow : ScriptableObject
{
    public List<LevelData> Levels = new List<LevelData>();
}
