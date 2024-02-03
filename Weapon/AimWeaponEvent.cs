using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class AimWeaponEvent : MonoBehaviour
{
    public event Action<AimWeaponEvent, AimWeaponEventArgs> OnWeaponAim;

    public void CallAimWeaponEvent(float mouseSensivity, float bottomClamp, float topClamp)
    {
        OnWeaponAim?.Invoke(this, new AimWeaponEventArgs()
        {
            mouseSensivity = mouseSensivity,
            bottomClamp = bottomClamp,
            topClamp = topClamp
        });
    }
}
public class AimWeaponEventArgs : EventArgs
{
    public float mouseSensivity;
    public float bottomClamp;
    public float topClamp;
}
