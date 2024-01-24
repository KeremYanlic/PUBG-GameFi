using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementByControllerEvent))]
[RequireComponent(typeof(CharacterController))]
[DisallowMultipleComponent]
public class MovementByController : MonoBehaviour
{
    private CharacterController characterController;
    private MovementByControllerEvent movementByControllerEvent;

    private void Awake()
    {
        //Load components
        characterController = GetComponent<CharacterController>();
        movementByControllerEvent = GetComponent<MovementByControllerEvent>();
    }
    private void OnEnable()
    {
        //Subscribe to movement by controller event
        movementByControllerEvent.OnMovementByController += MovementByControllerEvent_OnMovementByController;       
    }
    private void OnDisable()
    {
        //Unsubscribe from movement by controller event
        movementByControllerEvent.OnMovementByController -= MovementByControllerEvent_OnMovementByController;
    }



    private void MovementByControllerEvent_OnMovementByController(MovementByControllerEvent movementByControllerEvent, MovementByControllerEventArgs movementRef)
    {
        MoveCharacter(movementRef.direction, movementRef.moveSpeed);
    }

    // <summary>
    // Move the character
    // </summary>
    private void MoveCharacter(Vector3 direction, float moveSpeed)
    {
        characterController.Move(direction * moveSpeed * Time.deltaTime);
    }

    
}
