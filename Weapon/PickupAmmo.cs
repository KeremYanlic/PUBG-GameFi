using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PickupAmmoEvent))]
public class PickupAmmo : MonoBehaviour
{
    private Player player;
    private PickupAmmoEvent pickupAmmoEvent;

    private void Awake()
    {
        //Load components
        player = GetComponent<Player>();
        pickupAmmoEvent = GetComponent<PickupAmmoEvent>();
    }
    private void OnEnable()
    {
        // Subscribe to pick up ammo event
        pickupAmmoEvent.OnPickupAmmo += PickupAmmoEvent_OnPickupAmmo;
    }
    private void OnDisable()
    {
        // Unsubscribe from pick up ammo event
        pickupAmmoEvent.OnPickupAmmo -= PickupAmmoEvent_OnPickupAmmo;
    }

    private void PickupAmmoEvent_OnPickupAmmo(PickupAmmoEvent arg1, PickupAmmoEventArgs pickupAmmoEventArgs)
    {
        Weapon weapon = pickupAmmoEventArgs.weapon;

        weapon.weaponRemainingAmmo += pickupAmmoEventArgs.ammoBox.ammoAmount;

        Destroy(pickupAmmoEventArgs.ammoBox.gameObject);

       
    }

}
