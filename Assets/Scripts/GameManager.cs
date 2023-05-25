using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private InputActionReference exitInputActionReference;

    private void Start()
    {
        exitInputActionReference.action.performed += HandleExit;
    }

    private void HandleExit(InputAction.CallbackContext ctxt)
    {
        Application.Quit();
    }
}
