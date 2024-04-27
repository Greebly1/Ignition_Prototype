using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] bool triggerOnce = false;
    bool hasTriggered = false;

    Collider trigger;

    public UltEvent<GameObject> OnPlayerEntered;

    private void Awake()
    {
        trigger = GetComponent<Collider>();
        trigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerOnce)
        {
            if (!hasTriggered)
            {
                OnPlayerEntered.Invoke(other.gameObject);
            }

        } else
        {
            OnPlayerEntered.Invoke(other.gameObject);

        }


        hasTriggered = true;
    }
}
