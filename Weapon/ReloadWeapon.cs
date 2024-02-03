using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ReloadWeaponEvent))]
[RequireComponent(typeof(WeaponReloadedEvent))]
[RequireComponent(typeof(SetActiveWeaponEvent))]
public class ReloadWeapon : MonoBehaviour
{
    private ReloadWeaponEvent reloadWeaponEvent;
    private WeaponReloadedEvent weaponReloadedEvent;
    private SetActiveWeaponEvent setActiveWeaponEvent;
    private Coroutine reloadWeaponCoroutine;

    private void Awake()
    {
        //Load components
        reloadWeaponEvent = GetComponent<ReloadWeaponEvent>();
        weaponReloadedEvent = GetComponent<WeaponReloadedEvent>();
        setActiveWeaponEvent = GetComponent<SetActiveWeaponEvent>();
    }

    private void OnEnable()
    {
        // subscribe to reload weapon event
        reloadWeaponEvent.OnReloadWeapon += ReloadWeaponEvent_OnReloadWeapon;

        // subscribe to cancel reload weapon event
        reloadWeaponEvent.OnCancelReloadWeapon += ReloadWeaponEvent_OnCancelReloadWeapon;

        // subscribe to set active weapon event
        setActiveWeaponEvent.OnSetActiveWeapon += SetActiveWeaponEvent_OnSetActiveWeapon;
    }
    private void OnDisable()
    {
        // Unsubscribe from reload weapon event
        reloadWeaponEvent.OnReloadWeapon -= ReloadWeaponEvent_OnReloadWeapon;

        // Unsubscribe from cancel reload weapon event
        reloadWeaponEvent.OnCancelReloadWeapon -= ReloadWeaponEvent_OnCancelReloadWeapon;

        // Unsubscribe from set active weapon event
        setActiveWeaponEvent.OnSetActiveWeapon -= SetActiveWeaponEvent_OnSetActiveWeapon;
    }
    // <summary>
    // Handle reload weapon event
    // </summary>
    private void ReloadWeaponEvent_OnReloadWeapon(ReloadWeaponEvent arg1, ReloadWeaponEventArgs reloadWeaponEventArgs)
    {
        StartReloadWeapon(reloadWeaponEventArgs);
    }
    // <summary>
    // Start reloading the weapon
    // </summary>
    private void StartReloadWeapon(ReloadWeaponEventArgs reloadWeaponEventArgs)
    {
        if(reloadWeaponCoroutine != null)
        {
            StopCoroutine(reloadWeaponCoroutine);
        }
        reloadWeaponCoroutine = StartCoroutine(ReloadWeaponRoutine(reloadWeaponEventArgs.weapon));
    }
    // <summary>
    // Reload weapon coroutine
    // </summary>
    private IEnumerator ReloadWeaponRoutine(Weapon weapon)
    {
        // Play reload sound if there is one
        if(weapon.weaponDetailsSO.weaponReloadingSoundEffect != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(
                weapon.weaponDetailsSO.weaponReloadingSoundEffect,
                weapon.bulletSpawnPos.position);
        }

        // Set weapon as reloading
        weapon.isWeaponReloading = true;

        // Update reload progress timer
        while(weapon.weaponReloadTimer < weapon.weaponDetailsSO.weaponReloadTime)
        {
            weapon.weaponReloadTimer += Time.deltaTime;
            yield return null;
        }

        int clipRemainingAmmo = weapon.weaponClipRemainingAmmo;
        int totalRemainingAmmo = weapon.weaponRemainingAmmo;

        int ammoAmountToReload = weapon.weaponDetailsSO.weaponClipAmmoCapacity - clipRemainingAmmo;

        if(ammoAmountToReload > weapon.weaponRemainingAmmo)
        {
            weapon.weaponClipRemainingAmmo += weapon.weaponRemainingAmmo;
            weapon.weaponRemainingAmmo = 0;
        }
        else
        {
            weapon.weaponClipRemainingAmmo = weapon.weaponDetailsSO.weaponClipAmmoCapacity;
            weapon.weaponRemainingAmmo -= ammoAmountToReload;
        }
  
        //// If total ammo is to be increased then update
        //if(topUpAmmoPercent != 0)
        //{
        //    int ammoIncrease = Mathf.RoundToInt((weapon.weaponDetailsSO.weaponAmmoCapacity * topUpAmmoPercent) / 100f);

        //    int totalAmmo = weapon.weaponRemainingAmmo + ammoIncrease;

        //    if(totalAmmo > weapon.weaponDetailsSO.weaponAmmoCapacity)
        //    {
        //        weapon.weaponReloadTimer = weapon.weaponDetailsSO.weaponAmmoCapacity;
        //    }
        //    else
        //    {
        //        weapon.weaponRemainingAmmo = totalAmmo;
        //    }
        //}
        //weapon.weaponClipRemainingAmmo = weapon.weaponDetailsSO.weaponClipAmmoCapacity;
        
        //if(weapon.weaponRemainingAmmo >= weapon.weaponDetailsSO.weaponClipAmmoCapacity)
        //{
        //    weapon.weaponClipRemainingAmmo = weapon.weaponDetailsSO.weaponClipAmmoCapacity;
        //}
        //else
        //{
        //    weapon.weaponClipRemainingAmmo = weapon.weaponRemainingAmmo;
        //}

        // Reset weapon reload timer
        weapon.weaponReloadTimer = 0f;

        // Set weapon as not reloading
        weapon.isWeaponReloading = false;

        // Call weapon reloaded event
        weaponReloadedEvent.CallWeaponReloadedEvent(weapon);

    }
    // <summary>
    // Stop reloading 
    // </summary>
    private void ReloadWeaponEvent_OnCancelReloadWeapon(ReloadWeaponEvent arg1, CancelReloadWeaponEventArgs cancelReloadWeaponEventArgs)
    {
        // Stop reloading
        StopReloading(cancelReloadWeaponEventArgs.weapon, cancelReloadWeaponEventArgs.weaponClipAmmoAmount, cancelReloadWeaponEventArgs.weaponRemainingAmmoAmount);
    }

    private void SetActiveWeaponEvent_OnSetActiveWeapon(SetActiveWeaponEvent setActiveWeaponEvent, SetActiveWeaponEventArgs setActiveWeaponEventArgs)
    {
        if (setActiveWeaponEventArgs.weapon == null) return;

        if (setActiveWeaponEventArgs.weapon.isWeaponReloading)
        {
            if(reloadWeaponCoroutine != null)
            {
                StopCoroutine(reloadWeaponCoroutine);
            }
            reloadWeaponCoroutine = StartCoroutine(ReloadWeaponRoutine(setActiveWeaponEventArgs.weapon));
        }
    }

    public void StopReloading(Weapon weapon,int clipAmmoAmount, int weaponReaminingAmmo)
    {
        if(reloadWeaponCoroutine != null)
        {
            StopCoroutine(reloadWeaponCoroutine);
        }
        weapon.weaponReloadTimer = 0f;
        weapon.isWeaponReloading = false;

        weapon.weaponClipRemainingAmmo = clipAmmoAmount;
        weapon.weaponRemainingAmmo = weaponReaminingAmmo;
    }
   
}
