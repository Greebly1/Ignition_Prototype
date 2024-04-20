using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

//A projectile rocket, will move forward by a given speed,
//When it touches level geometry it invokes the 'hit' unityevent
[RequireComponent(typeof(Collider))]
public class Projectile_Rocket : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10f;
    public Vec3Event Hit = null; //unityEvent<Vector3>, passes the position of the colission

    Collider projectileCollider = null;

    #region monobehavior callbacks
    void OnEnable()
    {
        initializeCollider();
    }
    void FixedUpdate()
    {
        //propel the projectile forward this tick
        propelProjectile(Time.fixedDeltaTime);
    }
    //We expect this gameobject to be in the projectile layermask, so it will only trigger colision events when coliding with the level
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("entered collider");
        Vector3 colissionPosition = other.ClosestPoint(this.gameObject.transform.position);
        Hit.Invoke(colissionPosition);
    }

    #endregion


    /// <summary>
    /// Moves transform in its forward direction by projectileSpeed over a given time
    /// </summary>
    /// <param name="deltaTime">Amount of time projectile has moved</param>
    void propelProjectile(float deltaTime)
    {
        Vector3 deltaPosition = (projectileSpeed * this.gameObject.transform.forward ) * deltaTime;
        transform.position += deltaPosition;
    }

    /// <summary>
    /// Ensures the collider is not null and is in the correct states
    /// </summary>
    void initializeCollider()
    {
        projectileCollider = projectileCollider ?? GetComponent<Collider>();
        projectileCollider.enabled = true;
        projectileCollider.isTrigger = true;
    }
}
