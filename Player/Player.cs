using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(MovementByController))]
[RequireComponent(typeof(MovementByControllerEvent))]
[RequireComponent(typeof(JumpByController))]
[RequireComponent(typeof(JumpByControllerEvent))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(AimWeapon))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PickupAmmo))]
[RequireComponent(typeof(PickupAmmoEvent))]
[RequireComponent(typeof(PickupWeapon))]
[RequireComponent(typeof(PickupWeaponEvent))]
[RequireComponent(typeof(InteractionManager))]
[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(FireWeapon))]
[RequireComponent(typeof(SetActiveWeaponEvent))]
[RequireComponent(typeof(ActiveWeapon))]
[RequireComponent(typeof(WeaponFiredEvent))]
[RequireComponent(typeof(ReloadWeaponEvent))]
[RequireComponent(typeof(ReloadWeapon))]
[RequireComponent(typeof(WeaponReloadedEvent))]
[RequireComponent(typeof(DropWeapon))]
[RequireComponent(typeof(DropWeaponEvent))]
[RequireComponent(typeof(WeaponSwitcher))]
[RequireComponent(typeof(WeaponSwitcherEvent))]
[DisallowMultipleComponent]
#endregion REQUIRE COMPONENTS
public class Player : SingletonMonobehavior<Player>
{
    [HideInInspector] public MovementByControllerEvent movementByControllerEvent;
    [HideInInspector] public JumpByControllerEvent jumpByControllerEvent;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    [HideInInspector] public WeaponSlotManager weaponSlotManager;
    [HideInInspector] public PickupAmmoEvent pickupAmmoEvent;
    [HideInInspector] public PickupWeaponEvent pickupWeaponEvent;
    [HideInInspector] public SetActiveWeaponEvent setActiveWeaponEvent;
    [HideInInspector] public FireWeaponEvent fireWeaponEvent;
    [HideInInspector] public WeaponFiredEvent weaponFiredEvent;
    [HideInInspector] public ReloadWeaponEvent reloadWeaponEvent;
    [HideInInspector] public WeaponReloadedEvent weaponReloadedEvent;
    [HideInInspector] public ActiveWeapon activeWeapon;
    [HideInInspector] public DropWeaponEvent dropWeaponEvent;
    [HideInInspector] public InteractionManager interactionManager;
    [HideInInspector] public WeaponSwitcherEvent weaponSwitcherEvent;
    protected override void Awake()
    {
        movementByControllerEvent = GetComponent<MovementByControllerEvent>();
        jumpByControllerEvent = GetComponent<JumpByControllerEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        pickupAmmoEvent = GetComponent<PickupAmmoEvent>();
        pickupWeaponEvent = GetComponent<PickupWeaponEvent>();
        setActiveWeaponEvent = GetComponent<SetActiveWeaponEvent>();
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        weaponFiredEvent = GetComponent<WeaponFiredEvent>();
        reloadWeaponEvent = GetComponent<ReloadWeaponEvent>();
        weaponReloadedEvent = GetComponent<WeaponReloadedEvent>();
        activeWeapon = GetComponent<ActiveWeapon>();
        interactionManager = GetComponent<InteractionManager>();
        dropWeaponEvent = GetComponent<DropWeaponEvent>();
        weaponSwitcherEvent = GetComponent<WeaponSwitcherEvent>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }
}
