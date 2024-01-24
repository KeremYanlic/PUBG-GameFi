using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float bottomClamp = -90f;
    [SerializeField] private float topClamp = 90f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        //Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotation around the x axis (Look up and down)
        xRotation -= mouseY;

        // Clamp the rotation
        xRotation = Mathf.Clamp(xRotation, bottomClamp, topClamp);

        //Rotation around the y axis (Look left and right
        yRotation += mouseX;

        //Apply rotations to our transforms
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
