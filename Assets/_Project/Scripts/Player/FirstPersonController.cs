
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2.5f;
    public float runSpeed = 4.5f;
    public float gravity = -9.81f;

    [Header("Mouse")]
    public Transform playerCamera;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private float verticalVelocity;
    private float cameraPitch;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            Debug.LogError(
                "FirstPersonController: Player Camera is missing.",
                this
            );

            enabled = false;
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouse();
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void HandleMouse()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        float mouseX = Input.GetAxis("Mouse X")
                       * mouseSensitivity;

        float mouseY = Input.GetAxis("Mouse Y")
                       * mouseSensitivity;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);

        playerCamera.localRotation =
            Quaternion.Euler(cameraPitch, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }


    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction =
            transform.right * horizontal +
            transform.forward * vertical;

        direction.y = 0f;
        direction = Vector3.ClampMagnitude(direction, 1f);

        float speed = Input.GetKey(KeyCode.LeftShift)
            ? runSpeed
            : walkSpeed;

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 horizontalMotion = direction * speed;
        Vector3 verticalMotion = Vector3.up * verticalVelocity;

        CollisionFlags flags = controller.Move(
            (horizontalMotion + verticalMotion) * Time.deltaTime
        );

        if ((flags & CollisionFlags.Below) != 0
            && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        if (direction.sqrMagnitude > 0.01f &&
            controller.velocity.sqrMagnitude < 0.001f)
        {
            Debug.LogWarning(
                "Player movement blocked at: " + transform.position,
                this
            );
        }
    }
}