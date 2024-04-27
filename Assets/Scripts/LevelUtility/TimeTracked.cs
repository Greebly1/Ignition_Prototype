using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Time data generator
/// </summary>
public abstract class TimeTracker : MonoBehaviour
{
    public abstract ApplyableData GenerateTimeData();

    private void OnEnable()
    {
        Debug.Log("added time tracker");
        GameManager.Instance.CurrentLevel.timeTrackers.Add(this);
    }

    private void OnDisable()
    {
        Debug.Log("removed time tracker");
        GameManager.Instance.CurrentLevel.timeTrackers.Remove(this);
    }
}