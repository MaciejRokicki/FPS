using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSway : MonoBehaviour
{
    [SerializeField]
    private InputActionReference lookInputActionReference;

    [SerializeField]
    private float swayForce;
    [SerializeField]
    private float smoothness;

    private Vector2 lookDirection = Vector2.zero;

    private void Update()
    {
        Quaternion rotationX = Quaternion.AngleAxis(-lookDirection.y, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(lookDirection.x, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothness * 0.05f);

        lookDirection = lookInputActionReference.action.ReadValue<Vector2>() * swayForce;
    }
}
