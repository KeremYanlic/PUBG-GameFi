using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupAmmoEvent : MonoBehaviour
{
    public event Action<PickupAmmoEvent, PickupAmmoEventArgs> OnPickupAmmo;

    public void CallPickupAmmoEvent(Weapon weapon,AmmoBox ammoBox)
    {
        OnPickupAmmo?.Invoke(this, new PickupAmmoEventArgs()
        {
            weapon = weapon,
            ammoBox = ammoBox
        });
    }
}

public class PickupAmmoEventArgs : EventArgs
{
    public Weapon weapon;
    public AmmoBox ammoBox;
}
