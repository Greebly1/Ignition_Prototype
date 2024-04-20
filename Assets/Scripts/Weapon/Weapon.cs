using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] UnityEvent PrimaryAction;
    [SerializeField] float primaryActionCooldown = 1f;
    float primaryActionTimer = 0f; 
    public bool canPrimaryAction
    {
        get => primaryActionTimer <= 0f;
    }

    #region monobehavior callbacks
    private void Awake()
    {
        PrimaryAction.AddListener(OnPrimaryAction);
    }

    private void Update()
    {
        TickTimers(Time.deltaTime);
    }
    #endregion

    

    /// <summary>
    /// Invokes primary action if the cooldown is ready
    /// </summary>
    /// <returns>If it successfully invoked</returns>
    public bool TryInitiatePrimaryAction()
    {
        if (canPrimaryAction)
        {
            PrimaryAction.Invoke();
            return true; //early out
        }
        return false;
    }

    #region helper functions
    /// <summary>
    /// Tick all of the timers
    /// </summary>
    /// <param name="deltaTime">amount of time that has passed</param>
    void TickTimers(float deltaTime)
    {
        primaryActionTimer -= deltaTime;
    }
    void OnPrimaryAction()
    {
        primaryActionTimer = primaryActionCooldown; //reset the cooldown
        Debug.Log("Primary action");
    }
    #endregion
}
