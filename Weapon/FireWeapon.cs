using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActiveWeapon))]
[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(WeaponFiredEvent))]
[RequireComponent(typeof(ReloadWeaponEvent))]
[DisallowMultipleComponent]
public class FireWeapon : MonoBehaviour
{
    private float firePrechargeTimer = 0f;
    private float fireRateCoolDownTimer = 0f;
    private ActiveWeapon activeWeapon;
    private FireWeaponEvent fireWeaponEvent;
    private ReloadWeaponEvent reloadWeaponEvent;
    private WeaponFiredEvent weaponFiredEvent;
    private RecoilEvent recoilEvent;

    private void Awake()
    {
        //Load components
        activeWeapon = GetComponent<ActiveWeapon>();
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        reloadWeaponEvent = GetComponent<ReloadWeaponEvent>();
        weaponFiredEvent = GetComponent<WeaponFiredEvent>();
        recoilEvent = RecoilEvent.Instance;
    }

    private void OnEnable()
    {
        // Subscribe to fire weapon event
        fireWeaponEvent.OnFireWeapon += FireWeaponEvent_OnFireWeapon;
    }
    private void OnDisable()
    {
        // Unsubscribe from fire weapon event
        fireWeaponEvent.OnFireWeapon -= FireWeaponEvent_OnFireWeapon;
    }
    private void Update()
    {
        // Decrease cooldown timer
        fireRateCoolDownTimer -= Time.deltaTime;
    }
    // <summary>
    // Handle fire weapon event.
    // </summary>
    private void FireWeaponEvent_OnFireWeapon(FireWeaponEvent fireWeaponEvent, FireWeaponEventArgs fireWeaponEventArgs)
    {
        WeaponFire(fireWeaponEventArgs);
    }

    // <summary>
    // Fire weapon.
    // </summary>
    private void WeaponFire(FireWeaponEventArgs fireWeaponEventArgs)
    {
        //Handle weapon precharge timer.
        WeaponPreCharge(fireWeaponEventArgs);

        // Weapon fire
        if (fireWeaponEventArgs.fire)
        {
            // Test if weapon is ready to fire.
            if (IsWeaponReadyToFire())
            {
                //Fire ammo
                FireAmmo();

                ResetCooldownTimer();
            }
        }
    }

    // <summary>
    // Handle weapon precharge.
    // </summary>
    private void WeaponPreCharge(FireWeaponEventArgs fireWeaponEventArgs)
    {
        // Weapon precharge.
        if (fireWeaponEventArgs.firePreviousFrame)
        {
            // Decrease precharge timer if fire button held previous frame.
            firePrechargeTimer -= Time.deltaTime;
        }
        else
        {
            // else reset the precharge timer.
            ResetPrechargeTimer();
        }
    }

    // <summary>
    // Set up ammo using an ammo gameobject and component from the object pool.
    // </summary>
    private void FireAmmo()
    {
        BulletDetailsSO currentAmmo = activeWeapon.GetCurrentAmmo();

        if(currentAmmo != null)
        {
            // Fire ammo routine.
            StartCoroutine(FireAmmoRoutine(currentAmmo));
        }
    }

    // <summary>
    // Coroutine to spawn multiple ammo per shot if specified in the ammo details
    // </summary>
    private IEnumerator FireAmmoRoutine(BulletDetailsSO currentAmmo)
    {
        int ammoCounter = 0;

        // Get random ammo per shot
        int ammoPerShot = Random.Range(currentAmmo.ammoSpawnAmountMin, currentAmmo.ammoSpawnAmountMax + 1);

        // Get random interval between ammo
        float ammoSpawnInterval;

        if(ammoPerShot > 1)
        {
            ammoSpawnInterval = Random.Range(currentAmmo.ammoSpawnIntervalMin, currentAmmo.ammoSpawnIntervalMax);
        }
        else
        {
            ammoSpawnInterval = 0;
        }

        // Loop for number of ammo per shot
        while(ammoCounter < ammoPerShot)
        {
            ammoCounter++;

            // Get ammo prefab from array
            GameObject ammoPrefab = currentAmmo.ammoPrefabArray[0];

            // Get random speed value
            float ammoSpeed = Random.Range(currentAmmo.ammoSpeedMin, currentAmmo.ammoSpeedMax);

            // Get Gameobject with IFireable component
            IFireable ammo = (IFireable)PoolManager.Instance.ReuseComponent(ammoPrefab, activeWeapon.GetCurrentWeapon().bulletSpawnPos.position, Quaternion.identity);

            // Initialise Ammo
            ammo.InitialiseAmmo(currentAmmo, ammoSpeed, activeWeapon.GetCurrentWeapon().bulletSpawnPos.position);

            // Wait for ammo per shot timegap
            yield return new WaitForSeconds(ammoSpawnInterval);
        }

        // Recoil the weapon
        recoilEvent.CallRecoilEvent
        (
            activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponRecoilVector,
            activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponADSRecoil,
            activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponSnappiness,
            activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponReturnSpeed
        );

        activeWeapon.GetCurrentWeapon().weaponClipRemainingAmmo--;
        //activeWeapon.GetCurrentWeapon().weaponRemainingAmmo--;

        // Call weapon fired event
        weaponFiredEvent.CallWeaponFiredEvent(activeWeapon.GetCurrentWeapon());

        //Recoil.Instance.RecoilFire();

        // Weapon fired sound effect
        WeaponSoundEffect();

        // Display weapon shoot effect
        WeaponShootEffect();
    }
    // <summary>
    // Play weapon shooting sound effect
    // </summary>
    private void WeaponSoundEffect()
    {
        if(activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponFiringSoundEffect != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(
                activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponFiringSoundEffect,
                activeWeapon.GetCurrentWeapon().bulletSpawnPos.position
                );
        }
    }

    // <summary>
    // Display the weapon shoot effect
    // </summary>
    private void WeaponShootEffect()
    {
        if(activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponShootEffectFire != null && activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponShootEffectFire.weaponShootEffectPrefab != null)
        {
            //Get weapon shoot effect gameobject from the pool with particle system component
            WeaponShootEffect weaponShootEffect = (WeaponShootEffect)PoolManager.Instance.ReuseComponent
                (
                activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponShootEffectFire.weaponShootEffectPrefab,
                activeWeapon.GetCurrentWeapon().muzzleEffectSpawnPos.position,
                Quaternion.identity
                );


            //Set shoot effect
            //weaponShootEffect.SetShootEffect(activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponShootEffectFire);

            //Set gameobject active
            weaponShootEffect.gameObject.SetActive(true);
        }
    }

    // <summary>
    // Returns true if the weapon is ready to fire, else returns false
    // </summary>
    private bool IsWeaponReadyToFire()
    {
        // if there is no backup ammo and there is no ammo in clip then return false
        if (activeWeapon.GetCurrentWeapon().weaponRemainingAmmo <= 0 && activeWeapon.GetCurrentWeapon().weaponClipRemainingAmmo <= 0)
            return false;

        // if the weapon is reloading then return false
        if (activeWeapon.GetCurrentWeapon().isWeaponReloading)
            return false;

        // if the weapon isn't precharged or is cooling down then return false.
        if (firePrechargeTimer > 0f || fireRateCoolDownTimer > 0f)
            return false;

        // if no ammo in the clip then return false
        if (activeWeapon.GetCurrentWeapon().weaponClipRemainingAmmo <= 0)
        {
            return false;
        }
            
        // weapon is ready to fire - return true
        return true;
        
    }

    // <summary>
    // Reset cooldown timer
    // </summary>
    private void ResetCooldownTimer()
    {
        // Reset cooldown timer
        fireRateCoolDownTimer = activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponFireRate;
    }

    private void ResetPrechargeTimer()
    {
        // Reset precharge timer
        firePrechargeTimer = activeWeapon.GetCurrentWeapon().weaponDetailsSO.weaponPrechargeTime;
    }
}
