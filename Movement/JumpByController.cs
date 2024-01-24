using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JumpByControllerEvent))]
[RequireComponent(typeof(CharacterController))]
public class JumpByController : MonoBehaviour
{
    private CharacterController characterController;
    private JumpByControllerEvent jumpByControllerEvent;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        jumpByControllerEvent = GetComponent<JumpByControllerEvent>();
    }
    private void OnEnable()
    {
        //Subscribe to jump by controller event
        jumpByControllerEvent.OnJumpByController += JumpByControllerEvent_OnJumpByController;
    }
    private void OnDisable()
    {
        //Unsubscribe from jump by controller event
        jumpByControllerEvent.OnJumpByController -= JumpByControllerEvent_OnJumpByController;
    }

    private void JumpByControllerEvent_OnJumpByController(JumpByControllerEvent jumpByControllerEvent, JumpByControllerEventArgs jumpByControllerEventArgs)
    {
        Jump(jumpByControllerEventArgs.velocity);
    }

    // <summary>
    // Jump
    // </summary>
    private void Jump(Vector3 velocity)
    {
        characterController.Move(velocity * Time.deltaTime);
    }

}
