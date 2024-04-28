using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Time data generator
/// </summary>
public abstract class TimeTracker : MonoBehaviour
{
    public abstract ApplyableData GenerateTimeData();
    public bool TrackWhenActive = false;

    private void OnEnable()
    {
        if (TrackWhenActive)
        {
            StartCoroutine("ChangeTrackingNextFrame", true);
        }
        
    }

    private void OnDisable()
    {
        if ( TrackWhenActive)
        {
            EndTracking(); //TIL you cant use coroutines in ondisable, because this object will become null
        }
        
    }

    public void BeginTracking()
    {
        // Debug.Log("added time tracker");
        GameManager.Instance.CurrentLevel.timeTrackers.Add(this);
    }

    public void EndTracking()
    {
        //Debug.Log("removed time tracker");
        GameManager.Instance.CurrentLevel?.timeTrackers?.Remove(this);
    }

    IEnumerator ChangeTrackingNextFrame(bool isTracking)//I need to do this BS because of race errors in awake
    {
        yield return null; //wait one frame

        if (isTracking)
        {
            BeginTracking();
        } else
        {
            EndTracking();
        }
    }
}