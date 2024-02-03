using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireWeaponEvent : MonoBehaviour
{
    public event Action<FireWeaponEvent, FireWeaponEventArgs> OnFireWeapon;

    public void CallFireWeaponEvent(bool fire,bool firePreviousFrame)
    {
        OnFireWeapon?.Invoke(this,new FireWeaponEventArgs()
        {
            fire = fire,
            firePreviousFrame = firePreviousFrame
        });
    }

}
public class FireWeaponEventArgs : EventArgs
{
    public bool fire;
    public bool firePreviousFrame;
}
