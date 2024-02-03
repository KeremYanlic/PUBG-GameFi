using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSwitcherEvent : MonoBehaviour
{
    public event Action<WeaponSwitcherEvent, WeaponSwitcherEventArgs> OnSwitchWeapon;

    public void CallSwitchWeaponEvent(GameObject weaponToDrop,GameObject weaponToSwitch)
    {
        OnSwitchWeapon?.Invoke(this, new WeaponSwitcherEventArgs() { weaponToDrop = weaponToDrop, weaponToSwitch =weaponToSwitch });
    }
}
public class WeaponSwitcherEventArgs : EventArgs
{
    public GameObject weaponToDrop;
    public GameObject weaponToSwitch;
}
