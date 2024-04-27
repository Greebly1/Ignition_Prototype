using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActionProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] Transform firePoint = null;
    GameObjectPool pool = null;

    delegate GameObject Spawn();
    Spawn spawnMethod;

    private void Start()
    {
        pool = GameObjectPool.allPools[projectilePrefab]; //try to locate an object pool

        if (pool != null) //the pool exists
        {
            spawnMethod = PoolInstance;
        }
        else //the pool does not exist
        {
            spawnMethod = DefaultInstance;
        }
    }

    public void Initiate()
    {
        spawnMethod.Invoke().GetComponent<TimeTracker>().BeginTracking();

    }

    GameObject PoolInstance()
    {
        return pool.PoolInstantiate(firePoint.position, firePoint.rotation);
    }

    GameObject DefaultInstance()
    {
        return Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
