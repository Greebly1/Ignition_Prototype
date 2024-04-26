using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/SceneBindings")]
public class SceneBindings : ScriptableObject
{
    public SerializedDictionary<SceneType, SceneData> sceneDict;

    //TODO:
    //Add tutorial scene 
    //Add options menu scene 
}


[System.Serializable]
public class SceneData
{
    public string name;
    public Sprite thumbnail;
    public SceneType type;
}

public enum SceneType //helps alleviate string bindings so I don't typo
{
    Title,
    MainMenu,
    LevelMenu,
    PauseMenu,
    Credits,
    GameOver,
    Level,
    Sandbox
}