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

    }

    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = x * transform.right + z * transform.forward;

        movement = movement * speed * Time.deltaTime;
        myController.Move(movement);
    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        myCameraEyes.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
    }
}