using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableForDuration : MonoBehaviour
{
    public List<MonoBehaviour> Components = new List<MonoBehaviour>();
    public List<GameObject> GameObjects = new List<GameObject>();
    public float duration = 5;
    float timer = 5;
    Coroutine disableCouroutine = null;

    public void DisableObjects()
    {
        SetEnabledObjects(false);
        disableCouroutine = StartCoroutine("DisabledTimer");
    }


    private void Awake()
    {
        timer = duration;
    }

    bool TickTimer(float deltaTime)
    {
        timer -= deltaTime;
        return timer <= 0;
    }

    void SetEnabledObjects(bool enabled)
    {
        foreach (GameObject go in GameObjects)
        {
            go.SetActive(enabled);
        }

        foreach (MonoBehaviour component in Components) 
        { 
            component.enabled = enabled;
        }
    }

    IEnumerator DisabledTimer()
    {
        timer = duration;
        while (timer > 0)
        {
            TickTimer(Time.deltaTime);
            yield return null;
        }

        SetEnabledObjects(true);
        timer = duration;
    }
}
