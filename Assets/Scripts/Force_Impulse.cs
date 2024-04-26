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
