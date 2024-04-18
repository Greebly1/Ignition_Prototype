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
    public LayerMask hitLayerMask = 0; //collider layers that this projectile can hit
    public GameObjectEvent Hit = null; //unityEvent<gameobject>

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

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collission occured");

        //ensure the other collider is in the hit layermask
        //bool inLayer = hitLayerMask == (hitLayerMask | (1 << collision.gameObject.layer));
        //if (inLayer) { Hit?.Invoke(collision.gameObject); } //the projectile collider has hit something
    }
    #endregion


    /// <summary>
    /// Moves transform in its forward direction by projectileSpeed over a given time
    /// </summary>
    /// <param name="deltaTime">Amount of time projectile has moved</param>
    void propelProjectile(float deltaTime)
    {
        Vector3 deltaPosition = (projectileSpeed * Vector3.forward ) * deltaTime;
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