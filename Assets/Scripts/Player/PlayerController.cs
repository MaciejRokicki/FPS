using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;

    #region InputActionReferences
    [SerializeField]
    private InputActionReference movementInputActionReference;
    [SerializeField]
    private InputActionReference sprintActionReference;
    [SerializeField]
    private InputActionReference jumpInputActionReference;
    #endregion

    #region Settings
    [SerializeField]
    private float movementSpeed = 5.0f;
    [SerializeField]
    private float sprintBoost = 5.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;

    private float gravity = -9.81f;
    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity;
    #endregion

    private bool isGrounded;
    private bool isSprinting = false;
    private bool jump = false;

    #region Camera
    [SerializeField]
    private Camera playerCamera;

    private Vector2 lookDirection = Vector2.zero;
    [SerializeField]
    private Vector2 mouseCameraSensitivity = new Vector2(4.0f, 3.0f);
    [SerializeField]
    private Vector2 gamepadCameraSensitivity = new Vector2(80.0f, 60.0f);
    private float cameraVerticalRotation = 0.0f;
    [SerializeField]
    private float cameraVerticalRotationClamp = 30.0f;
    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();

        if(playerInput.currentControlScheme == "Gamepad")
        {
            lookDirection = Gamepad.current.rightStick.ReadValue() * gamepadCameraSensitivity;
        }
        else if(playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            lookDirection = Mouse.current.delta.ReadValue() * mouseCameraSensitivity;
        }

        HandleRotation();
    }

    private void MoveInputPerformed(CallbackContext ctxt)
    {
        Vector2 callbackValue = ctxt.ReadValue<Vector2>();
        moveDirection = new Vector3(callbackValue.x, 0.0f, callbackValue.y);
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

    private void SprintInputPerformed(CallbackContext ctxt)
    {
        if (!isSprinting)
        {
            movementSpeed += sprintBoost;
            isSprinting = true;
        }
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

        if (isSprinting && moveDirection == Vector3.zero)
        {
            movementSpeed -= sprintBoost;
            isSprinting = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = transform.rotation * moveDirection * movementSpeed;
        move.y = verticalVelocity;

        characterController.Move(move * Time.deltaTime);
    }

    private void HandleRotation()
    {
        lookDirection *= Time.deltaTime;
        transform.Rotate(Vector3.up, lookDirection.x);

        cameraVerticalRotation -= lookDirection.y;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -cameraVerticalRotationClamp, cameraVerticalRotationClamp);

        Vector3 cameraRotation = playerCamera.transform.eulerAngles;
        cameraRotation.x = cameraVerticalRotation;
        playerCamera.transform.eulerAngles = cameraRotation;
    }

    private void OnEnable()
    {
        movementInputActionReference.action.performed += MoveInputPerformed;
        movementInputActionReference.action.canceled += MoveInputCanceled;
        jumpInputActionReference.action.performed += JumpInputPerformed;
        sprintActionReference.action.performed += SprintInputPerformed;
    }

    private void OnDisable()
    {
        movementInputActionReference.action.performed -= MoveInputPerformed;
        movementInputActionReference.action.canceled -= MoveInputCanceled;
        jumpInputActionReference.action.performed -= JumpInputPerformed;
        sprintActionReference.action.performed -= SprintInputPerformed;
    }
}