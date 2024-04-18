using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ForceMoveable
{
    public Vector3 AddForce(Vector3 force); //adds a force to an object that is force movable, and returns the new force
}
