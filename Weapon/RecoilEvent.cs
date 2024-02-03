using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecoilEvent : SingletonMonobehavior<RecoilEvent>
{
    public event Action<RecoilEvent, RecoilEventArgs> OnRecoil;

    public void CallRecoilEvent(Vector3 weaponRecoil, Vector3 weaponAdsRecoil,float snappiness, float returnSpeed)
    {
        OnRecoil?.Invoke(this,new RecoilEventArgs() 
        {
            weaponRecoil = weaponRecoil,
            weaponAdsRecoil = weaponAdsRecoil,
            snappiness = snappiness,
            returnSpeed = returnSpeed
        });
    }
   
}
public class RecoilEventArgs : EventArgs
{
    public Vector3 weaponRecoil;
    public Vector3 weaponAdsRecoil;
    public float snappiness;
    public float returnSpeed;
}
