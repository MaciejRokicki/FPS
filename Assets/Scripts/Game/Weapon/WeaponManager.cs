using UnityEngine.InputSystem;

public class WeaponManager : Singleton<WeaponManager>
{
    public InputActionReference FireInputActionReference;
    public InputActionReference ReloadInputActionReference;
    public InputActionReference ChangeFireModeActionReference;
}
