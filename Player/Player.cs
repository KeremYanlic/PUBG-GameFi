using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(MovementByController))]
[RequireComponent(typeof(MovementByControllerEvent))]
[RequireComponent(typeof(JumpByController))]
[RequireComponent(typeof(JumpByControllerEvent))]
[RequireComponent(typeof(PlayerController))]
[DisallowMultipleComponent]
#endregion REQUIRE COMPONENTS
public class Player : MonoBehaviour
{
    [HideInInspector] public MovementByControllerEvent movementByControllerEvent;
    [HideInInspector] public JumpByControllerEvent jumpByControllerEvent;
    private void Awake()
    {
        movementByControllerEvent = GetComponent<MovementByControllerEvent>();
        jumpByControllerEvent = GetComponent<JumpByControllerEvent>();
    }
}
