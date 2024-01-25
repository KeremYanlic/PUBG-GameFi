using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator animator;
    public bool isActiveWeapon;

    [Header("References")]
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private GameObject bulletPrefab;
    
    [Header("Variables - General Settings")]
    [SerializeField] private float bulletVelocity = 30f;
    [SerializeField] private float bulletPrefabLifeTime = 3f;

    // Shooting
    [Header("Variables - Shooting")]
    [SerializeField] private float shootingDelay = 2f;
    private bool isShooting, readyToShoot, allowReset = true;

    // Burst
    [Header("Variables - Burst")]
    public int bulletsPerBurst = 3;
    public int burstBulletLeft;

    // Spread
    [Header("Variables - Spread")]
    [SerializeField] private float spreadIntensity;
    public ShootingMode currentShootingMode;
    public WeaponModel weaponModel;

    [Header("Variables - Reloading")]
    [SerializeField] private float reloadTime;
    public int magazineSize, bulletsLeft;
    [SerializeField] private bool isReloading;

    [Header("Variables - Effects")]
    [SerializeField] private GameObject muzzleEffect;

    [Header("Variables - WeaponSlotSettings")]
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        readyToShoot = true;
        burstBulletLeft = bulletsPerBurst;
        isReloading = false;
        bulletsLeft = magazineSize;
    }
    private void Update()
    {
        if(isActiveWeapon == false) { return; }


        if(currentShootingMode == ShootingMode.Auto)
        {
            //Holding Down Left Mouse Button
            isShooting = Input.GetMouseButton(0) && !isReloading;
        }
        else if(currentShootingMode == ShootingMode.Single || 
            currentShootingMode == ShootingMode.Burst)
        {
            //Clicking Left Mouse Button Once
            isShooting = Input.GetMouseButtonDown(0) && !isReloading;
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false && WeaponManager.Instance.CheckAmmoLeftFor(weaponModel) > 0)
        {
            Reload();
        }
        if (readyToShoot && isShooting && bulletsLeft > 0)
        {
            burstBulletLeft = bulletsPerBurst;
            FireWeapon();
        }
        else if(readyToShoot && isShooting && bulletsLeft == 0)
        {
            SoundManager.Instance.emptyMagazineSound.Play();
        }
     
    }
    private void FireWeapon()
    {
        bulletsLeft--;

        animator.SetTrigger("Recoil");
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.Instance.PlayShootingSound(weaponModel);

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);

        // Positing the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        // Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity,ForceMode.Impulse);

        // Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet,bulletPrefabLifeTime));

        Recoil.Instance.RecoilFire();
        // Checking if we are done shooting
        if(allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Burst Mode
        if(currentShootingMode == ShootingMode.Burst && burstBulletLeft > 1)
        {
            burstBulletLeft--;
            Invoke("FireWeapon",shootingDelay);
        }
    }
    private void Reload()
    {
        SoundManager.Instance.PlayReloadSound(weaponModel);
        animator.SetTrigger("Reload");


        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        if(WeaponManager.Instance.CheckAmmoLeftFor(weaponModel) > magazineSize)
        {
            bulletsLeft = magazineSize;
            WeaponManager.Instance.DecreaseTotalAmmo(bulletsLeft, weaponModel);
        }
        else
        {
            bulletsLeft = WeaponManager.Instance.CheckAmmoLeftFor(weaponModel);
            WeaponManager.Instance.DecreaseTotalAmmo(bulletsLeft, weaponModel);

        }

        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
 

    private Vector3 CalculateDirectionAndSpread()
    {
        // Shooting from the middle of the screen to check where are pointing at
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // If hitting something then return hit.point else return a point at 100 distance units along the ray. 
        Vector3 targetPoint = Physics.Raycast(ray, out hit) ? hit.point : ray.GetPoint(100);

        Vector3 direction = targetPoint - bulletSpawnPos.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        // Returning the shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bulletToDestroy,float bulletPrefabLifeTime)
    {
        yield return new WaitForSeconds(bulletPrefabLifeTime);
        Destroy(bulletToDestroy);
    }
}
