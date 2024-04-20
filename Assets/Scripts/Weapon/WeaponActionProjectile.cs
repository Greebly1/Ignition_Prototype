using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActionProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] Transform firePoint = null;

    public void Initiate()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
