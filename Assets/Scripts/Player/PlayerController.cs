using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;

    #region InputActionReferences
    [Header("Input Action References")]
    [SerializeField]
    private InputActionReference movementInputActionReference;
    [SerializeField]
    private InputActionReference lookInputActionReference;
    [SerializeField]
    private InputActionReference sprintHoldActionReference;
    [SerializeField]
    private InputActionReference sprintToggleActionReference;
    [SerializeField]
    private InputActionReference jumpInputActionReference;
    #endregion

    #region Settings
    [Header("Player settings")]
    [SerializeField]
    private float movementSpeed = 5.0f;
    [SerializeField]
    private float sprintBoost = 5.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    private float gravity = -9.81f;
    private float verticalVelocity;
    #endregion

    private bool isGrounded;
    private bool isSprinting = false;
    private bool jump = false;

    #region Camera
    [Header("Camera settings")]
    [SerializeField]
    private Camera playerCamera;
    private Vector2 lookDirection = Vector2.zero;
    [SerializeField]
    private Vector2 mouseSensitivity = new Vector2(4.0f, 3.0f);
    [SerializeField]
    private Vector2 gamepadSensitivity = new Vector2(160.0f, 120.0f);
    private float cameraVerticalRotation = 0.0f;
    [SerializeField]
    private float cameraVerticalRotationClamp = 30.0f;
    #endregion

    private void OnEnable()
    {
        movementInputActionReference.action.performed += MoveInputPerformed;
        movementInputActionReference.action.canceled += MoveInputCanceled;
        jumpInputActionReference.action.performed += JumpInputPerformed;
        sprintHoldActionReference.action.performed += SprintHoldInputPerformed;
        sprintHoldActionReference.action.canceled += SprintHoldInputCanceled;
        sprintToggleActionReference.action.canceled += SprintToggleInputCanceled;
    }

    private void OnDisable()
    {
        movementInputActionReference.action.performed -= MoveInputPerformed;
        movementInputActionReference.action.canceled -= MoveInputCanceled;
        jumpInputActionReference.action.performed -= JumpInputPerformed;
        sprintHoldActionReference.action.performed -= SprintHoldInputPerformed;
        sprintHoldActionReference.action.canceled -= SprintHoldInputCanceled;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            lookDirection = lookInputActionReference.action.ReadValue<Vector2>() * gamepadSensitivity * Time.deltaTime;
        }
        else if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            lookDirection = lookInputActionReference.action.ReadValue<Vector2>() * mouseSensitivity;
        }

        HandleMovement();
        HandleRotation();
    }

    private void MoveInputPerformed(CallbackContext ctxt)
    {
        moveDirection.x = ctxt.ReadValue<Vector2>().x;
        moveDirection.z = ctxt.ReadValue<Vector2>().y;
    }

    private void MoveInputCanceled(CallbackContext ctxt)
    {
        moveDirection = Vector3.zero;
    }

    private void JumpInputPerformed(CallbackContext ctxt)
    {
        if (isGrounded)
        {
            jump = true;
        }
    }

    private void SprintHoldInputPerformed(CallbackContext ctxt)
    {
        HandleToggleSprint();
    }

    private void SprintHoldInputCanceled(CallbackContext ctxt)
    {
        if(isSprinting)
        {
            isSprinting = false;
            movementSpeed -= sprintBoost;
        }
    }

    private void SprintToggleInputCanceled(CallbackContext ctxt)
    {
        HandleToggleSprint();
    }

    private void HandleMovement()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
            verticalVelocity = 0.0f;

            if (jump)
            {
                verticalVelocity += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

                jump = false;
            }
        }

        if (isSprinting && (moveDirection.z <= 0.0f || moveDirection == Vector3.zero))
        {
            HandleToggleSprint();
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = transform.rotation * moveDirection * movementSpeed;
        move.y = verticalVelocity;

        characterController.Move(move * Time.deltaTime);
    }

    private void HandleToggleSprint()
    {
        isSprinting = !isSprinting;

        movementSpeed += isSprinting ? sprintBoost : -sprintBoost;
    }

    private void HandleRotation()
    {
        transform.Rotate(Vector3.up, lookDirection.x);

        cameraVerticalRotation -= lookDirection.y;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -cameraVerticalRotationClamp, cameraVerticalRotationClamp);

        Vector3 cameraRotation = playerCamera.transform.eulerAngles;
        cameraRotation.x = cameraVerticalRotation;
        playerCamera.transform.eulerAngles = cameraRotation;
    }
}