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

    //State machine
    private enum State { Normal, Building };
    private State state;

    // Placing tower
    public TowerBase towerBase;
    private TowerBase towerBaseInstance;
    public GameObject tower;
    public static int towerCost = 100;

    // Change Weapon

    public GunSystem[] guns;
    private GunSystem gun;
    private int gunNumber = 0;
    private List<GameObject> towers = new List<GameObject>();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myController = GetComponent<CharacterController>();
        state = State.Normal;
        gun = guns[0];

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                PlayerMovement();
                CameraMovement();
                Jump();
                StateManager();
                GunManager();
                break;
            case State.Building:
                PlayerMovement();
                CameraMovement();
                Jump();
                PlacingTower();
                StateManager();
                break;
            default:
                break;
        }


        if (state == State.Building)
        {

        }
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
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * Physics.gravity.y);
        }

        // Add gravity influence on the player
        verticalVelocity.y += 0.5f * Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravity;
        myController.Move(verticalVelocity * Time.deltaTime);
    }

    private void PlacingTower()
    {
        RaycastHit hit;
        if (Physics.Raycast(myCameraEyes.position, myCameraEyes.forward, out hit, groundLayer) && hit.collider.CompareTag("Ground"))
        {
            {
                if (towerBaseInstance == null) towerBaseInstance = Instantiate(towerBase, hit.point, Quaternion.identity);
                else
                {
                    towerBaseInstance.gameObject.SetActive(true);
                    towerBaseInstance.transform.position = hit.point;
                }

                if (Input.GetMouseButtonDown(0) && !towerBaseInstance.isColliding && GameManager.getMoney() >= towerCost)
                {
                    towers.Add(Instantiate(tower, hit.point, Quaternion.identity));
                    GameManager.RemoveMoney(towerCost);
                }

            }
        }
        else if (towerBaseInstance != null) towerBaseInstance.gameObject.SetActive(false);
    }
    private void StateManager()
    {
        if (state == State.Normal && Input.GetKeyDown(KeyCode.E) && !Input.GetMouseButton(0))
        {
            state = State.Building;
            gun.gameObject.SetActive(false);
        }
        else if (state == State.Building && Input.GetKeyDown(KeyCode.E) && !Input.GetMouseButton(0))
        {
            state = State.Normal;
            gun.gameObject.SetActive(true);
            if (towerBaseInstance != null)
            {
                Destroy(towerBaseInstance.gameObject);
                towerBaseInstance = null;
            }
        }
    }

    private void GunManager()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            gunNumber++;
            gun.gameObject.SetActive(false);
            if (gunNumber >= guns.Length)
            {
                gunNumber = 0;
            }
            gun = guns[gunNumber];
            gun.gameObject.SetActive(true);
            gun.SetReadyToShoot(true);
            foreach (GameObject tower in towers)
            {
                tower.GetComponent<Tower>().ChangeProjectile(gunNumber);
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            gunNumber--;
            gun.gameObject.SetActive(false);
            if (gunNumber < 0)
            {
                gunNumber = guns.Length - 1;
            }
            gun = guns[gunNumber];
            gun.gameObject.SetActive(true);
            gun.SetReadyToShoot(true);
            foreach (GameObject tower in towers)
            {
                tower.GetComponent<Tower>().ChangeProjectile(gunNumber);
            }
        }
    }

    IEnumerator SwitchTimer()
    {
        yield return new WaitForSeconds(0.5f);
    }


}
