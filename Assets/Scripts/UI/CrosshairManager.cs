using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairManager : Singleton<CrosshairManager>
{
    [SerializeField]
    private Animator crosshairAnimator;

    //TODO: usunac potem
    [SerializeField]
    private InputActionReference fireReference;

    private void OnEnable()
    {
        fireReference.action.performed += OnFire;
    }

    private void OnDisable()
    {
        fireReference.action.performed -= OnFire;
    }

    private void OnFire(InputAction.CallbackContext ctxt)
    {
        crosshairAnimator.Play("OnHitAnimation");
    }
}
