using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player))]
[RequireComponent(typeof(DropWeaponEvent))]
[DisallowMultipleComponent]
public class DropWeapon : MonoBehaviour
{
    [SerializeField] private float dropForce;
    [SerializeField] private float dropTorque;

    private DropWeaponEvent dropWeaponEvent;
    private Player player;
    private void Awake()
    {
        // Load components
        dropWeaponEvent = GetComponent<DropWeaponEvent>();
        player = GetComponent<Player>();
    }
    private void OnEnable()
    {
        // Subscribe to drop weapon event
        dropWeaponEvent.OnDropWeapon += DropWeaponEvent_OnDropWeapon;
    }
    private void OnDisable()
    {
        // Unsubscribe from drop weapon event
        dropWeaponEvent.OnDropWeapon -= DropWeaponEvent_OnDropWeapon;
    }
    private void DropWeaponEvent_OnDropWeapon(DropWeaponEvent dropWeaponEvent)
    {
        // if there is no weapon in active slot then return
        if (player.weaponSlotManager.IsActiveSlotFull() == false) return;

        // Get weapon to drop
        GameObject weaponToDrop = player.weaponSlotManager.GetActiveSlot().transform.GetChild(0).gameObject;

        // get weapon reference
        Weapon weapon = weaponToDrop.GetComponent<Weapon>();

        // Drop the weapon
        JustDropWeapon(weaponToDrop);

        // If weapon is reloading then cancel reloading
        if (player.activeWeapon.GetCurrentWeapon().isWeaponReloading)
        {
            player.reloadWeaponEvent.CallCancelReloadWeaponEvent(weapon, weapon.weaponClipRemainingAmmo, weapon.weaponRemainingAmmo);
        }
        
        GameObject slotHasWeapon = player.weaponSlotManager.GetNotAvailableMainWeaponSlot();
        if (slotHasWeapon != null)
        {
            // if there is another slot then set that slot as active
            player.weaponSlotManager.SetActiveSlot(slotHasWeapon);
        }
        else
        {
            // if there is not then set active weapon as null
            player.setActiveWeaponEvent.CallSetActiveWeaponEvent(null);
        }
    }
  
    public void JustDropWeapon(GameObject weaponToDrop)
    {
        // Detach the weapon from the slot
        weaponToDrop.transform.SetParent(null, true);
        Rigidbody weaponRB = weaponToDrop.GetComponent<Rigidbody>();
        if (weaponRB != null)
        {
            weaponRB.isKinematic = false;
            // Add force or torque for a more realistic drop effect
            weaponRB.AddForce(Vector3.down * dropForce, ForceMode.Impulse);
            weaponRB.AddTorque(Random.insideUnitSphere * dropTorque, ForceMode.Impulse);
        }
    }
}
