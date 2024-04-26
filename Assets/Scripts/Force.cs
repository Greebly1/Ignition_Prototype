using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Force")]
public class Force : ScriptableObject
{
    public float radius = 10f;
    public float strength = 20f;
    public AnimationCurve forceFalloff;

    public void ApplyForce(ForceInteraction interactionData)
    {
        Vector3 pointOnCollider = interactionData.receiverCollider.ClosestPoint(interactionData.forceOrigin);
        //First calculate the distance and determine the strength of the force with the falloff
        float dist = Vector3.Distance(pointOnCollider, interactionData.forceOrigin);
        float distPercent = Mathf.Clamp(1 - (dist / radius), 0.0f, 1.0f);
        float finalForceStrength = forceFalloff.Evaluate(distPercent) * strength;

        Vector3 forceDirection = Vector3.Normalize(interactionData.receiverCollider.transform.position - interactionData.forceOrigin); //calculate the direction of the force

        //Debug.Log("Distance: " + dist + " DistPercent: " + distPercent + " final force str: " + finalForceStrength + " forceDir: " + forceDirection + "Final force: " + forceDirection * finalForceStrength);
        interactionData.receiver.AddForce(forceDirection * finalForceStrength); //tell the receiver to take that force
    }
}