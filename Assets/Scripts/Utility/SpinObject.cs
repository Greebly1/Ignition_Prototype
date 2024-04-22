using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Purpose, spin a rocket projectile on its forward axis, maybe useful for other spinning things too
/// </summary>
public class SpinObject : MonoBehaviour
{
    public float spinSpeed = 5f;
    public bool spinCounterClockwise = false;

    public void Update()
    {
        SpinTransform(this.transform, Time.deltaTime);
    }

    //spin the transform on its local forward axis
    void SpinTransform(Transform spinningTransform, float deltaTime)
    {
        Vector3 RotationEuler = new Vector3(0, 0, spinSpeed * deltaTime);
        spinningTransform.Rotate(RotationEuler, Space.Self);
    }
}
