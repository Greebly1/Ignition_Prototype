using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Purpose is to destroy a gameobject after a given amount of seconds has elapsed
/// </summary>
public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float TimeToLive = 5f;
    float timeAlive = 0f;

    [SerializeField] bool startTimeOnAwake = true;

    [SerializeField] GameObject ObjectToDestroy = null;
    [SerializeField] bool IsInObjectPool = false;

    bool timerRunning = false;

    private void Awake()
    {
        timerRunning = startTimeOnAwake;
    }

    private void OnEnable()
    {
        timeAlive = 0;
    }

    private void Update()
    {
        if (timerRunning)
        {
            TickTimer();
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    private void TickTimer()
    {
        timeAlive += Time.deltaTime;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (timeAlive > TimeToLive)
        {
            KillObject(ObjectToDestroy);
            
        }
    }

    void KillObject(GameObject target)
    {
        //if it is in an object pool, simply disable it
        if (IsInObjectPool)
        {
            Debug.Log("IAH$E");
            target.SetActive(false);
        } else
        {
            Destroy(target);
        }
    }
}
