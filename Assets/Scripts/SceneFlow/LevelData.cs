using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public SceneData sceneInfo;
    public Difficulty levelDifficulty = Difficulty.easy;
    
}

public enum Difficulty
{
    easy,
    medium,
    hard
}
