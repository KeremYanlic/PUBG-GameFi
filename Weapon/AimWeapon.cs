using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimWeaponEvent))]
[DisallowMultipleComponent]
public class AimWeapon : MonoBehaviour
{
    private AimWeaponEvent aimWeaponEvent;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Awake()
    {
        //Load components
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
    }
    private void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        //Subscribe to aim weapon event
        aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
    }
    private void OnDisable()
    {
        //Unsubscribe from aim weapon event
        aimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
    }
  
    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs aimWeaponEventArgs)
    {
        Aim(aimWeaponEventArgs.mouseSensivity, aimWeaponEventArgs.bottomClamp, aimWeaponEventArgs.topClamp);
    }
    private void Aim(float mouseSensitivity, float bottomClamp,float topClamp)
    {
        //Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotation around the x axis (Look up and down)
        xRotation -= mouseY;

        // Clamp the rotation
        xRotation = Mathf.Clamp(xRotation, bottomClamp, topClamp);

        //Rotation around the y axis (Look left and right
        yRotation += mouseX;

        //Apply rotations to our transforms
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    
}
