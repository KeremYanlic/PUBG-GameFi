using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponDetailsSO weaponDetailsSO;
    public float weaponReloadTimer;
    public int weaponClipRemainingAmmo;
    public int weaponRemainingAmmo;
    public bool isWeaponReloading;

    public Transform bulletSpawnPos;
    public Transform muzzleEffectSpawnPos;
}
