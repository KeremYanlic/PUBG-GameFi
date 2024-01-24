using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementByControllerEvent : MonoBehaviour
{
    public event Action<MovementByControllerEvent, MovementByControllerEventArgs> OnMovementByController;

    public void CallMovementByControllerEvent(Vector3 direction, float moveSpeed)
    {
        OnMovementByController?.Invoke(this, new MovementByControllerEventArgs()
        {
            direction = direction,
            moveSpeed = moveSpeed
        });
    }
}
public class MovementByControllerEventArgs : EventArgs
{
    public Vector3 direction;
    public float moveSpeed;
}
