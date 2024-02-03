using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PickupWeaponEvent))]
[DisallowMultipleComponent]
public class PickupWeapon : MonoBehaviour
{
    private Player player;
    private PickupWeaponEvent pickupWeaponEvent;
    private void Awake()
    {
        //Load components
        player = GetComponent<Player>();
        pickupWeaponEvent = GetComponent<PickupWeaponEvent>();
    }
    private void OnEnable()
    {
        // Subscribe to pick up weapon event
        pickupWeaponEvent.OnPickupWeapon += PickupWeaponEvent_OnPickupWeapon;
    }
    private void OnDisable()
    {
        // Unsubscribe to pick up weapon event
        pickupWeaponEvent.OnPickupWeapon -= PickupWeaponEvent_OnPickupWeapon;

    }

    private void PickupWeaponEvent_OnPickupWeapon(PickupWeaponEvent pickupWeaponEvent, PickupWeaponEventArgs pickupWeaponEventArgs)
    {
        //Gameobject ref
        GameObject weaponToPickup = pickupWeaponEventArgs.weaponToPickup;

        //Weapon script ref
        Weapon weapon = weaponToPickup.GetComponent<Weapon>();

        switch (weapon.weaponDetailsSO.weaponType)
        {
            case WeaponType.MainWeapon:
                //Check if both slots empty
                bool isBothSlotEmpty = player.weaponSlotManager.IsBothSlotEmpty();
                
                if (isBothSlotEmpty)
                {
                    // put weapon into first slot
                    PutWeaponIntoFirstSlot(weaponToPickup, weapon);

                    player.setActiveWeaponEvent.CallSetActiveWeaponEvent(weapon);
                }
                //Check if one of the slots empty
                else if(!isBothSlotEmpty && !player.weaponSlotManager.IsBothSlotFull())
                {
                    // get an available slot
                    GameObject availableSlot = player.weaponSlotManager.GetAvailableMainWeaponSlot();

                    // Pickup weapon
                    PutWeaponIntoAvailableSlot(weaponToPickup, weapon, availableSlot);

                    player.setActiveWeaponEvent.CallSetActiveWeaponEvent(weapon);
                }
                break;
            case WeaponType.Pistol:
                // Check if pistol slot empty first
                GameObject pistolSlot = player.weaponSlotManager.GetPistolSlot();
                if(pistolSlot == null) { return; }

                // Pickup pistol
                PutPistolIntoPistolSlot(weaponToPickup, weapon, pistolSlot);

                player.setActiveWeaponEvent.CallSetActiveWeaponEvent(weapon);
                break;
        }
    }

    // <summary>
    // Put weapon into first slot
    // </summary>
    private void PutWeaponIntoFirstSlot(GameObject weaponToPickup,Weapon weapon) 
    {
        GameObject firstSlot = player.weaponSlotManager.GetFirstWeaponSlot();

        weaponToPickup.transform.SetParent(firstSlot.transform, false);
        weaponToPickup.transform.localPosition = weapon.weaponDetailsSO.weaponSpawnPosition;
        weaponToPickup.transform.localRotation = Quaternion.Euler(weapon.weaponDetailsSO.weaponSpawnRotation);
        weaponToPickup.GetComponent<Rigidbody>().isKinematic = true;

        player.weaponSlotManager.SetActiveSlot(firstSlot);
        
    }

    // <summary>
    // Put weapon into available slot
    // </summary>
    private void PutWeaponIntoAvailableSlot(GameObject weaponToPickup,Weapon weapon, GameObject availableSlot)
    {
        weaponToPickup.transform.SetParent(availableSlot.transform, false);
        weaponToPickup.transform.localPosition = weapon.weaponDetailsSO.weaponSpawnPosition;
        weaponToPickup.transform.localRotation = Quaternion.Euler(weapon.weaponDetailsSO.weaponSpawnRotation);
        weaponToPickup.GetComponent<Rigidbody>().isKinematic = true;

        player.weaponSlotManager.SetActiveSlot(availableSlot);
    }

    // <summary>
    // Put pistol into pistol slot
    // </summary>
    private void PutPistolIntoPistolSlot(GameObject weaponToPickup,Weapon weapon, GameObject pistolSlot)
    {
        weaponToPickup.transform.SetParent(pistolSlot.transform, false);
        weaponToPickup.transform.localPosition = weapon.weaponDetailsSO.weaponSpawnPosition;
        weaponToPickup.transform.localRotation = Quaternion.Euler(weapon.weaponDetailsSO.weaponSpawnRotation);
        weaponToPickup.GetComponent<Rigidbody>().isKinematic = true;

        player.weaponSlotManager.SetActiveSlot(pistolSlot);

    }
}
