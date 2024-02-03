using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class PickupWeaponEvent : MonoBehaviour
{
    public event Action<PickupWeaponEvent, PickupWeaponEventArgs> OnPickupWeapon;

    public void CallPickupWeaponEvent(GameObject weaponToPickup)
    {
        OnPickupWeapon?.Invoke(this, new PickupWeaponEventArgs() { weaponToPickup = weaponToPickup });
    }
}
public class PickupWeaponEventArgs : EventArgs
{
    public GameObject weaponToPickup;
}
