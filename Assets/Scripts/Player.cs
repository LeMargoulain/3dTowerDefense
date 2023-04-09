using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player Movement
    public float speed = 12.5f, mouseSensitivity = 100f;
    private CharacterController myController;
    //public Transform myCameraHead;
    //private float cameraVerticalRotation;
    void Start()
    {
        myController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

    }

    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = x * transform.right + z * transform.forward;

        movement = movement * speed * Time.deltaTime;
        myController.Move(movement);
    }
}
