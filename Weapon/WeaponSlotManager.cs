using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponSlotList;

    private Player player;
    private GameObject activeWeaponSlot;
    private int currentSlotIndex = 0;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    private void Start()
    {
        activeWeaponSlot = weaponSlotList[0].gameObject;
        ArrangeSlotActiveness(activeWeaponSlot);
    }
  
    public void SwitchWeaponSlot()
    {
        // If there is a gun in first slot then set first slot as active slot
        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsFirstSlotAvailable())
        {
            SetActiveSlot(weaponSlotList[0].gameObject);
        }
        // If there is a gun in second slot then set second slot as active slot
        if (Input.GetKeyDown(KeyCode.Alpha2) && !IsSecondSlotAvailable())
        {
            SetActiveSlot(weaponSlotList[1].gameObject);
        }
        // If there is a pistol in pistol slot then set pistol slot as active slot
        if (Input.GetKeyDown(KeyCode.Alpha3) && !IsPistolSlotAvailable())
        {
            SetActiveSlot(weaponSlotList[2].gameObject);
        }

        //Disable later when character press a button or something like that
    }

    public void SetActiveSlot(GameObject slot)
    {
        activeWeaponSlot = slot;

        Weapon weapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
        player.setActiveWeaponEvent.CallSetActiveWeaponEvent(weapon);

        ArrangeSlotActiveness(activeWeaponSlot);
    }
    // This is gonna be an event later
   private void ArrangeSlotActiveness(GameObject activeWeaponSlot)
    {
        foreach(GameObject weaponSlot in weaponSlotList)
        {
            if(weaponSlot == activeWeaponSlot)
            {
                weaponSlot.gameObject.SetActive(true);
            }
            else
            {
                weaponSlot.gameObject.SetActive(false);
            }
        }
    }

    public GameObject GetActiveSlot()
    {
        return activeWeaponSlot;
    }
    public bool IsActiveSlotFull()
    {
        return activeWeaponSlot.transform.childCount > 0;
    }
    public GameObject GetAvailableMainWeaponSlot()
    {
        if(IsFirstSlotAvailable() && IsSecondSlotAvailable())
        {
            return weaponSlotList[0];
        }
        else if(IsFirstSlotAvailable() && !IsSecondSlotAvailable())
        {
            return weaponSlotList[0];

        }
        else if(!IsFirstSlotAvailable() && IsSecondSlotAvailable())
        {
            return weaponSlotList[1];
        }
        else
        {
            return null;
        }
    }
    public GameObject GetNotAvailableMainWeaponSlot()
    {
        if (!IsFirstSlotAvailable() && IsSecondSlotAvailable())
        {
            return weaponSlotList[0];
        }
        else if (IsFirstSlotAvailable() && !IsSecondSlotAvailable())
        {
            return weaponSlotList[1];
        }
        else
        {
            return null;
        }
    }
    // <summary>
    // Check if the both main weapon slot is empty
    // </summary>
    public bool IsBothSlotEmpty()
    {
        return weaponSlotList[0].transform.childCount == 0 && weaponSlotList[1].transform.childCount == 0;
    }
    // <summary>
    // Check if the both main weapon slot is full
    // </summary>
    public bool IsBothSlotFull()
    {
        return weaponSlotList[0].transform.childCount > 0 && weaponSlotList[1].transform.childCount > 0;

    }
    public GameObject GetFirstWeaponSlot()
    {
        return weaponSlotList[0];
    }
    public GameObject GetPistolSlot()
    {
        return weaponSlotList[2];
    }

    private bool IsFirstSlotAvailable()
    {
        return weaponSlotList[0].transform.childCount == 0;
    }
    private bool IsSecondSlotAvailable()
    {
        return weaponSlotList[1].transform.childCount == 0;

    }
    private bool IsPistolSlotAvailable()
    {
        return weaponSlotList[2].transform.childCount == 0;
    }
}
