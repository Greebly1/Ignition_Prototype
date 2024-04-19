using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject heldWeaponEmpty = null;
    Weapon _heldWeapon = null;
    Weapon heldWeapon
    {
        get { 
            if (_heldWeapon == null)
            {
                _heldWeapon = heldWeaponEmpty?.GetComponentInChildren<Weapon>();
            }
            return _heldWeapon; 
        }
    }

    bool _isPrimaryFiring = false;
    bool isPrimaryFiring
    {
        get { return _isPrimaryFiring; }
        set {
            if (_isPrimaryFiring != value)
            {
                //value changed event
                if(value) //the player just pressed the button
                {
                    heldWeapon?.TryInitiatePrimaryAction();
                } else //the player just released the button
                {

                }
            }
            _isPrimaryFiring = value; 
        }
    }

    #region Monobehavior Callbacks
    private void Start()
    {
        if (heldWeaponEmpty == null) { 
            Debug.LogError("HeldItemController has a null weapon empty"); 
            return; //early out
        }

        _heldWeapon = heldWeapon; //initializing with setter
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            isPrimaryFiring = true;
        } else
        {
            isPrimaryFiring = false;
        }
    }
    #endregion
}
