using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JumpByControllerEvent : MonoBehaviour
{
    public event Action<JumpByControllerEvent, JumpByControllerEventArgs> OnJumpByController;

    public void CallJumpByControllerEvent(Vector3 velocity)
    {
        OnJumpByController?.Invoke(this, new JumpByControllerEventArgs() { velocity = velocity });
    }
}
public class JumpByControllerEventArgs : EventArgs
{
    public Vector3 velocity;
}
