using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetailsSO_", menuName = "Scriptable Objects/Weapon/WeaponDetailsSO")]
public class WeaponDetailsSO : ScriptableObject
{
    #region Header BASE DETAILS
    [Space(10)]
    [Header("WEAPON BASE DETAILS")]
    #endregion

    #region Tooltip
    [Tooltip("Weapon Name")]
    #endregion 
    public string weaponName;

    #region Tooltip
    [Tooltip("The prefab for the weapon.")]
    #endregion
    public GameObject weaponPrefab;

    #region Tooltip
    [Tooltip("Weapon type")]
    #endregion
    public WeaponType weaponType;

    #region Header WEAPON CONFIGURATION
    [Space(10)]
    [Header("WEAPON CONFIGURATION")]
    #endregion

    #region Tooltip
    [Tooltip("Weapon Spawn Position")]
    #endregion
    public Vector3 weaponSpawnPosition;

    #region Tooltip
    [Tooltip("Weapon Spawn Rotation")]
    #endregion
    public Vector3 weaponSpawnRotation;

    #region Tooltip
    [Tooltip("Weapon Shoot Position")]
    #endregion
    public Vector3 weaponShootPosition;


    #region Tooltip
    [Tooltip("Weapon Effect Position ")]
    #endregion
    public Vector3 weaponEffectPosition;


    #region Tooltip
    [Tooltip("Weapon Snappiness")]
    #endregion
    public float weaponSnappiness;

    #region Tooltip
    [Tooltip("Return speed")]
    #endregion
    public float weaponReturnSpeed;


    #region Tooltip
    [Tooltip("Weapon current ammo")]
    #endregion 
    public BulletDetailsSO weaponCurrentAmmo;

    #region Tooltip
    [Tooltip("Weapon shoot effect SO - contains particle effect parameters to be used in conjunction with the weaponShootEffectPrefab ")]
    #endregion Tooltip
    public WeaponShootEffectSO weaponShootEffectSmoke;

    #region Tooltip
    [Tooltip("Weapon shoot effect SO - contains particle effect parameters to be used in conjuntion with the weaponShootEffectPrefab ")]
    #endregion Tooltip
    public WeaponShootEffectSO weaponShootEffectFire;

    
    #region Tooltip
    [Tooltip("The firing sound effect SO for the weapon")]
    #endregion Tooltip
    public SoundEffectSO weaponFiringSoundEffect;

   
    #region Tooltip
    [Tooltip("The reloading sound effect SO for the weapon")]
    #endregion Tooltip
    public SoundEffectSO weaponReloadingSoundEffect; 
    

    #region Header WEAPON OPERATING VALUES
    [Space(10)]
    [Header("WEAPON OPERATING VALUES")]
    #endregion
    #region Tooltip
    [Tooltip("Weapon recoil vector")]
    #endregion
    public Vector3 weaponRecoilVector;

    #region Tooltip
    [Tooltip("Weapon aim down recoil vector")]
    #endregion
    public Vector3 weaponADSRecoil;

    #region Tooltip
    [Tooltip("The weapon capacity - shots before a reload")]
    #endregion
    public int weaponClipAmmoCapacity = 6;

    
    #region Tooltip
    [Tooltip("Weapon Fire Rate - 0.2 means 5 shots a second")]
    #endregion
    public float weaponFireRate = 0.2f;

    #region Tooltip
    [Tooltip("Weapon Precharge Time - time in seconds to hold fire button down before firing")]
    #endregion
    public float weaponPrechargeTime = 0f;

    #region Tooltip
    [Tooltip("This is the weapon reload time in seconds")]
    #endregion
    public float weaponReloadTime = 1f;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        UtilsClass.ValidateCheckEmptyString(this, nameof(weaponName), weaponName);
        UtilsClass.ValidateCheckNullValue(this, nameof(weaponPrefab), weaponPrefab);
        UtilsClass.ValidateCheckPositiveValue(this, nameof(weaponClipAmmoCapacity), weaponClipAmmoCapacity, false);
        UtilsClass.ValidateCheckPositiveValue(this, nameof(weaponFireRate), weaponFireRate, false);
    }
#endif
    #endregion
}
