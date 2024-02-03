using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class ReloadWeaponEvent : MonoBehaviour
{
    public event Action<ReloadWeaponEvent, ReloadWeaponEventArgs> OnReloadWeapon;
    public event Action<ReloadWeaponEvent, CancelReloadWeaponEventArgs> OnCancelReloadWeapon;

    // <summary>
    // Sepcify the weapon to have it's clip reloaded. If the total ammo is also to be increased then specify the topUpAmmoPercent.
    // </summary>
    public void CallReloadWeaponEvent(Weapon weapon)
    {
        OnReloadWeapon?.Invoke(this, new ReloadWeaponEventArgs()
        {
            weapon = weapon,
        });
    }

    // </summary>
    // Function for canceling reloading for such cases like dropping weapon.
    // </summary>
    public void CallCancelReloadWeaponEvent(Weapon weapon, int weaponClipAmmoAmount, int weaponRemainingAmmoAmount)
    {
        OnCancelReloadWeapon?.Invoke(this, new CancelReloadWeaponEventArgs()
        {
            weapon = weapon,
            weaponClipAmmoAmount = weaponClipAmmoAmount,
            weaponRemainingAmmoAmount = weaponRemainingAmmoAmount
        });
    }
}
public class ReloadWeaponEventArgs : EventArgs
{
    public Weapon weapon;
}

public class CancelReloadWeaponEventArgs : EventArgs
{
    public Weapon weapon;
    public int weaponClipAmmoAmount;
    public int weaponRemainingAmmoAmount;
}
