using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsUI : MonoBehaviour
{
    [SerializeField]
    private InputActionReference toggleControlsInputActionReference;
    [SerializeField]
    private GameObject controlsGameObject;

    private void Awake()
    {
        toggleControlsInputActionReference.action.performed += ToggleControlsUI;
    }

    private void ToggleControlsUI(InputAction.CallbackContext ctxt)
    {
        controlsGameObject.SetActive(!controlsGameObject.activeSelf);
    }
}
