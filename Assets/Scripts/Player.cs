using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player Movement
    public float speed = 12.5f, mouseSensitivity = 100f;
    private CharacterController myController;
    public Transform myCameraEyes;
    private float cameraVerticalRotation;
    // Gravity
    private Vector3 verticalVelocity;
    public float gravity = 9.8f;
    // Jump
    public Transform ground;
    public float jumpHeight = 0.02f, groundDistance = 10.0f;
    public LayerMask groundLayer;
    private bool readyToJump;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CameraMovement();
        Jump();
    }

    private void PlayerMovement()
    {
        //Move the player on the xz plane
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = x * transform.right + z * transform.forward;

        movement = movement * speed * Time.deltaTime;
        myController.Move(movement);
        //Add gravity influence on the player
        verticalVelocity.y += 0.5f * Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravity;
        myController.Move(verticalVelocity);
    }

    private void CameraMovement()
    {
        //Rotate the camera along X and Y axes
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        myCameraEyes.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
    }

    private void Jump()
    {
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * Physics.gravity.y);
        }

        // Add gravity influence on the player
        verticalVelocity.y += 0.5f * Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravity;
        myController.Move(verticalVelocity * Time.deltaTime);
    }
}
