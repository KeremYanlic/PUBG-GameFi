using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : SingletonMonobehavior<InteractionManager>
{
    private Player player;
    private Weapon hoveredWeapon = null;
    private AmmoBox hoveredAmmoBox = null;

    private bool interactingWithGun = false;
    private bool interactingWithAmmoBox = false;
   
    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        HandleRaycastInteraction();
      
    }
    // <summary>
    // Handle all of the raycast interactions
    // </summary>
    private void HandleRaycastInteraction()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;
            HandleWeaponInteraction(objectHitByRaycast);
            HandleAmmoBoxInteraction(objectHitByRaycast);
        }
        else
        {
            ResetInteractions();
        }
    }

    // <summary>
    // Handle weapon interactions
    // </summary>
    private void HandleWeaponInteraction(GameObject objectHitByRaycast)
    {
        Weapon weapon = objectHitByRaycast.GetComponent<Weapon>();

        if (weapon)
        {
            // Hover weapon
            SetHoveredWeapon(weapon);
        }
        else
        {
            // Reset any kind of interaction with a gun
            ResetWeaponInteraction();
        }
    }
    // <summary>
    // Handle ammo box interactions
    // </summary>
    private void HandleAmmoBoxInteraction(GameObject objectHitByRaycast)
    {
        AmmoBox ammoBox = objectHitByRaycast.GetComponent<AmmoBox>();

        if (ammoBox)
        {
            // Hover ammo box
            SetHoveredAmmoBox(ammoBox);
        }
        else
        {
            // Reset any kind of interaction with an ammo box
            ResetAmmoBoxInteractions();
        }
    }

    private void SetHoveredWeapon(Weapon weapon)
    {
        ResetInteractions();
        hoveredWeapon = weapon;
        hoveredWeapon.GetComponent<Outline>().enabled = true;
        interactingWithGun = true;
    }

    private void ResetWeaponInteraction()
    {
        if (hoveredWeapon)
        {
            hoveredWeapon.GetComponent<Outline>().enabled = false;
        }
        interactingWithGun = false;
    }
    private void SetHoveredAmmoBox(AmmoBox ammoBox)
    {
        ResetInteractions();
        hoveredAmmoBox = ammoBox;
        hoveredAmmoBox.GetComponent<Outline>().enabled = true;
        interactingWithAmmoBox = true;
    }

    private void ResetAmmoBoxInteractions()
    {
        if (hoveredAmmoBox)
        {
            hoveredAmmoBox.GetComponent<Outline>().enabled = false;
        }
        interactingWithAmmoBox = false;
    }


    // <summary>
    // Reset interactions
    // </summary>
    private void ResetInteractions()
    {
        ResetWeaponInteraction();
        ResetAmmoBoxInteractions();
    }

    public bool IsInteractingWithGun()
    {
        return interactingWithGun;
    }
    public GameObject GetWeaponInteractingWith()
    {
        return hoveredWeapon?.gameObject;
    }

    public bool IsInteractingWithAmmoBox()
    {
        return interactingWithAmmoBox;
    }
    public AmmoBox GetAmmoBoxInteractingWith()
    {
        return hoveredAmmoBox;
    }
}
