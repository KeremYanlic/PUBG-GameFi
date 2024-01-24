using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUDManagerUI : MonoBehaviour
{
    public static HUDManagerUI Instance;

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;

    public Sprite emptySlot;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unactiveWeapon = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();

        if (activeWeapon)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeftFor(activeWeapon.weaponModel)}";

            WeaponModel weaponModel = activeWeapon.weaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(weaponModel);

            activeWeaponUI.sprite = GetWeaponSprite(weaponModel);

            if (unactiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unactiveWeapon.weaponModel);
            }
        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";

            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;
        }
    }

    private Sprite GetWeaponSprite(WeaponModel model)
    {
        switch (model)
        {
            case WeaponModel.Pistol1911:
                return Instantiate(Resources.Load<GameObject>("Pistol1911_Weapon")).GetComponent<SpriteRenderer>().sprite;
            case WeaponModel.M16:
                return Instantiate(Resources.Load<GameObject>("M16_Weapon")).GetComponent<SpriteRenderer>().sprite;

            default: return null;
        }
    }
    private Sprite GetAmmoSprite(WeaponModel model)
    {
        switch (model)
        {
            case WeaponModel.Pistol1911:
                return Instantiate(Resources.Load<GameObject>("Pistol1911_Ammo")).GetComponent<SpriteRenderer>().sprite;
            case WeaponModel.M16:
                return Instantiate(Resources.Load<GameObject>("M16_Ammo")).GetComponent<SpriteRenderer>().sprite;

            default: return null;
        }

    }
    private GameObject GetUnActiveWeaponSlot()
    {
        foreach(GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if(weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }
        // this will never happen, but we need to return something 
        return null;
    }
}
