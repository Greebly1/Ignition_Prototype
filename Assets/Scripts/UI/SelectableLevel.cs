using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectableLevel : MonoBehaviour
{
    #region Editor Vars
    [SerializeField] TextMeshProUGUI LevelName;
    [SerializeField] Image LevelThumbnail;
    [SerializeField] Sprite defaultThumbnail; //thumbnail to use if it is passed a null value
    #endregion

    LevelData _data;
    [HideInInspector] public LevelData data
    {
        get { return _data; }
        set 
        { 
            _data = value;
            SetUiData(data);
        }
    }

    void SetUiData(LevelData UIData)
    {
        LevelName.text = UIData.sceneInfo.name ?? "Level Name";
        LevelThumbnail.sprite = UIData.sceneInfo.thumbnail ?? defaultThumbnail;
    }

    public void OnSelectLevel()
    {
        if (_data != null)
        {
            MainMenuManager.Instance?.LoadLevel(data.sceneInfo);
        }
    }
}
