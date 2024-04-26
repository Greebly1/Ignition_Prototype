using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] LevelFlow Levels;
    [SerializeField] GameObject SelectableLevelPrefab;
    [SerializeField] GameObject LevelSelectPool;

    #region Shorthand Getters
    List<SelectableLevel> CurrentSelectableLevels
    {
        get { 
            return LevelSelectPool.GetComponentsInChildren<SelectableLevel>().ToList<SelectableLevel>();
        }
    }
    #endregion


    private void Awake()
    {
        ClearSelectableLevels();
        foreach (LevelData level in Levels.Levels)
        {
            MakeSelectableLevel(level);
        }    
    }

    void ClearSelectableLevels()
    {
        foreach (SelectableLevel level in CurrentSelectableLevels)
        {
            Destroy(level.gameObject);
        }
    }

    void MakeSelectableLevel(LevelData level)
    {
        SelectableLevel newLevel = Instantiate(SelectableLevelPrefab, LevelSelectPool.transform).GetComponent<SelectableLevel>();
        newLevel.data = level;

        List<MonoBehaviour> allPrefabBehaviors = newLevel.GetComponentsInChildren<MonoBehaviour>().ToList<MonoBehaviour>();
        foreach (MonoBehaviour behavior in allPrefabBehaviors)
        {
            behavior.enabled = true;
        }
    }
}
