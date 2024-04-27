using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularArrayTest : MonoBehaviour
{
    public CircularArray<int> array = new CircularArray<int>();

    public bool tickTimer = false;

    public float insertDelay = 1;
    float timeTillInsert = 0;

    int insertionCount = 0;

    private void Update()
    {
        if (tickTimer)
        {
            TickTimer(Time.deltaTime);

        }
    }

    void TickTimer(float deltaTime)
    {
        timeTillInsert -= deltaTime;
        if (timeTillInsert < 0)
        {
            timeTillInsert = insertDelay;
            InsertItem();
        }
    }

    public void LogArray()
    {
        array.LogSelf();
    }

    public void InsertItem()
    {
        insertionCount++;
        array.Insert(insertionCount);
    }

    public void IncrementIndex()
    {
        array.currIndex++;
    }

    public void DecrementIndex()
    {
        array.currIndex--;
    }

    public void ClampArray()
    {
        array.ClampArrayToCurrentIndex();
    }
}
