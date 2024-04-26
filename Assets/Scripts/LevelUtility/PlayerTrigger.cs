using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    Collider trigger;

    public UltEvent<GameObject> OnPlayerEntered;

    private void Awake()
    {
        trigger = GetComponent<Collider>();
        trigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerEntered.Invoke(other.gameObject);
    }
}
