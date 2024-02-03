using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropWeaponEvent : MonoBehaviour
{
    public event Action<DropWeaponEvent> OnDropWeapon;

    
    public void CallDropWeaponEvent()
    {
        OnDropWeapon?.Invoke(this);
    }
}

