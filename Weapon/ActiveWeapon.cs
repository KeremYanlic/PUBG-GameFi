using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SetActiveWeaponEvent))]
[DisallowMultipleComponent]
public class ActiveWeapon : MonoBehaviour
{
    private SetActiveWeaponEvent setActiveWeaponEvent;
    private Weapon currentWeapon;

    private void Awake()
    {
        // Load components
        setActiveWeaponEvent = GetComponent<SetActiveWeaponEvent>();
    }

    private void OnEnable()
    {
        // Subscribe to set active weapon event
        setActiveWeaponEvent.OnSetActiveWeapon += SetActiveWeaponEvent_OnSetActiveWeapon;
    }
    private void OnDisable()
    {
        // Unsubscribe from set active weapon event
        setActiveWeaponEvent.OnSetActiveWeapon -= SetActiveWeaponEvent_OnSetActiveWeapon;
    }

    private void SetActiveWeaponEvent_OnSetActiveWeapon(SetActiveWeaponEvent setActiveWeaponEvent, SetActiveWeaponEventArgs setActiveWeaponEventArgs)
    {
        SetWeapon(setActiveWeaponEventArgs.weapon);
    }

    private void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }

    public BulletDetailsSO GetCurrentAmmo()
    {
        return currentWeapon.weaponDetailsSO.weaponCurrentAmmo;
    }
    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
    public Vector3 GetShootPosition()
    {
        return currentWeapon.weaponDetailsSO.weaponShootPosition;
    }
    public Vector3 GetShootEffectPosition()
    {
        return currentWeapon.weaponDetailsSO.weaponEffectPosition;
    }

}
