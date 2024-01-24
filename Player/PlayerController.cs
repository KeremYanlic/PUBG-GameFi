using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private Player player;

    private CharacterController characterController;

    [Header("References")]
    [SerializeField] private Transform groundCheckTransform;
    

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float gravity = -9.81f * 2;
    [SerializeField] private float jumpHeight = 3f;

    [Header("Ground Check Variables")]
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    private float runningMultipler = 0f;

    private void Awake()
    {
        player = GetComponent<Player>();

        //Load components
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundDistance, groundMask); 

        // Reset the default velocity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y -= 2f;
        }

        MovementInput();

        JumpInput();

        MovingIdentifier();
    }
    private void MovementInput()
    {
        runningMultipler = Mathf.Clamp(runningMultipler, 0f, 3f);

        //Getting the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Creating the moving vector
        Vector3 moveDirection = transform.right * x + transform.forward * z;
         
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        runningMultipler += (isRunning ? 1 : -1);
        //Actually moving the player
        player.movementByControllerEvent.CallMovementByControllerEvent(moveDirection, moveSpeed + runningMultipler);
    }
    private void JumpInput()
    {
        //Check if the player can jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        //Executing the jump
        player.jumpByControllerEvent.CallJumpByControllerEvent(velocity);
    }
    private void MovingIdentifier()
    {
        isMoving = lastPosition != gameObject.transform.position && isGrounded;

        lastPosition = gameObject.transform.position;
    }

}
