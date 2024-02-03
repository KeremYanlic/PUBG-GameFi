using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponSwitcherEvent))]
[RequireComponent(typeof(DropWeapon))]
[RequireComponent(typeof(PickupWeaponEvent))]
[DisallowMultipleComponent]
public class WeaponSwitcher : MonoBehaviour
{
    private WeaponSwitcherEvent weaponSwitcherEvent;
    private DropWeapon dropWeapon;
    private PickupWeaponEvent pickupWeaponEvent;
    private void Awake()
    {
        //Load components
        weaponSwitcherEvent = GetComponent<WeaponSwitcherEvent>();
        dropWeapon = GetComponent<DropWeapon>();
        pickupWeaponEvent = GetComponent<PickupWeaponEvent>();
    }
    private void OnEnable()
    {
        // Subscribe to weapon switcher event
        weaponSwitcherEvent.OnSwitchWeapon += WeaponSwitcherEvent_OnSwitchWeapon;
    }
    private void OnDisable()
    {
        // Unsubscribe from weapon switcher event
        weaponSwitcherEvent.OnSwitchWeapon -= WeaponSwitcherEvent_OnSwitchWeapon;
    }

    private void WeaponSwitcherEvent_OnSwitchWeapon(WeaponSwitcherEvent arg1, WeaponSwitcherEventArgs weaponSwitcherEventArgs)
    {
        dropWeapon.JustDropWeapon(weaponSwitcherEventArgs.weaponToDrop);

        pickupWeaponEvent.CallPickupWeaponEvent(weaponSwitcherEventArgs.weaponToSwitch);
    }
}
