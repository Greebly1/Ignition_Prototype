using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// An implementation of an arbritary force giver,
/// performs a sphere overlap searching for ForceMovables, then applies hthe force to it
/// </summary>
public class ImpulseForce : MonoBehaviour
{
    public Force force;

    /// <summary>
    /// Performs the impulse force
    /// </summary>
    /// <param name="position">the origin of the force</param>
    public void Initiate(Vector3 position)
    {
        Collider[] potentialHits = Physics.OverlapSphere(position, force.radius); //get everything in a radius of the force

        List<ForceInteraction> hits = new List<ForceInteraction>(); //make a list of ForceMovable objects found in the above operation
        foreach (Collider currentCollider in potentialHits)
        {
            ForceMoveable colliderCast = currentCollider.GetComponent<ForceMoveable>();
            if (colliderCast != null) { 
                hits.Add(new ForceInteraction(position, currentCollider, colliderCast)); 
            }

            colliderCast = currentCollider.GetComponentInParent<ForceMoveable>();
            if (colliderCast != null) { hits.Add(new ForceInteraction(position, currentCollider, colliderCast)); }
        }

        if (hits.Count > 0) //we hit something
        {
            foreach (ForceInteraction currentHit in hits) //apply the force to all of the hit forcemovable objects
            {
                force.ApplyForce(currentHit);
            }
        } 
    }
}

public struct ForceInteraction
{
    public Vector3 forceOrigin;
    public Collider receiverCollider;
    public ForceMoveable receiver;

    public ForceInteraction(Vector3 forceOrigin, Collider receiverCollider, ForceMoveable receiver)
    {
        this.forceOrigin = forceOrigin;
        this.receiverCollider = receiverCollider;
        this.receiver = receiver;
    }
}

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