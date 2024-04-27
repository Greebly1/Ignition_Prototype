using Fragsurf.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SurfCharacter))]
public class PlayerTracker : TimeTracker
{
    SurfCharacter thisPlayer;
    private void Start()
    {
        thisPlayer = GetComponent<SurfCharacter>();
    }

    public override ApplyableData GenerateTimeData()
    {
        return new PlayerTimeData(
            new TimeTrackedTransform( //<-- this is just absurd
                this.transform,
                this.transform.position,
                this.transform.rotation),
            thisPlayer,
            thisPlayer.moveData.velocity);
    }
}

public class PlayerTimeData : ApplyableData
{
    TimeTrackedTransform transformData;
    SurfCharacter dataRecipient;

    Vector3 velocity;

    public void ApplyData()
    {
        transformData.ApplyData();
        dataRecipient.moveData.velocity = this.velocity;
    }

    public PlayerTimeData(TimeTrackedTransform TransformData, SurfCharacter DataRecipient, Vector3 currVelocity)
    {
        transformData = TransformData;
        dataRecipient = DataRecipient;
        velocity = currVelocity;
    }
}
