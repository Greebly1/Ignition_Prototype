using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTracker : TimeTracker 
{
    public override ApplyableData GenerateTimeData()
    {
        return new TimeTrackedTransform(this.transform, transform.position, transform.rotation);
    }
}
