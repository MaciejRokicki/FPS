using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : Singleton<WeaponManager>
{
    public InputActionReference FireInputActionReference;
    public InputActionReference ReloadInputActionReference;
    public InputActionReference ChangeFireModeActionReference;
    public InputActionReference NextWeaponActionReference;

    [SerializeField]
    private Animator weaponSocketAnimator;

    [SerializeField]
    private Weapon[] weapons;
    [SerializeField]
    private int currentWeaponId = 0;

    private void OnEnable()
    {
        NextWeaponActionReference.action.performed += NextWeapon;
    }

    private void OnDisable()
    {
        NextWeaponActionReference.action.performed -= NextWeapon;
    }

    private void NextWeapon(InputAction.CallbackContext ctxt)
    {
        if(ctxt.ReadValue<float>() > 0)
        {
            weapons[currentWeaponId].gameObject.SetActive(false);
            currentWeaponId = (currentWeaponId + 1) % weapons.Length;
            weapons[currentWeaponId].gameObject.SetActive(true);
        }
        else
        {
            weapons[currentWeaponId].gameObject.SetActive(false);

            currentWeaponId--;
            if(currentWeaponId < 0)
            {
                currentWeaponId = weapons.Length - 1;
            }

            weapons[currentWeaponId].gameObject.SetActive(true);
        }

        weaponSocketAnimator.Play("SwapWeapon");
    }
}