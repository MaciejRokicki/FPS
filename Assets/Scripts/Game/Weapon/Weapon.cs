using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private WeaponManager weaponManager;
    private CrosshairManager crosshairManager;

    [SerializeField]
    private WeaponStatistics statistics;

    private WeaponFireModeBaseStrategy weaponFireModeStrategy;

    [SerializeField]
    private int ammo;
    private float firerate;
    public float Firerate
    {
        get { return firerate; }
        set
        {
            if(value < 0)
                firerate = 0;
            else
                firerate = value;
        }
    }
    private float reloadingTime;

    private bool isShooting;
    public bool IsShooting
    {
        get { return isShooting; }
        set { isShooting = value; }
    }
    private bool isReloading = false;
    public bool CanShoot
    {
        get
        {
            if(!isReloading && IsShooting && Firerate == 0 && ammo > 0)
            {
                return true;
            }

            return false;
        }
    }

    private void OnEnable()
    {
        weaponManager.FireInputActionReference.action.performed += FirePerformed;
        weaponManager.FireInputActionReference.action.canceled += FireCanceled;
        weaponManager.ReloadInputActionReference.action.performed += ReloadPerformed;
        weaponManager.ChangeFireModeActionReference.action.performed += ChangeFireModePerformed;
    }

    private void OnDisable()
    {
        weaponManager.FireInputActionReference.action.performed -= FirePerformed;
        weaponManager.FireInputActionReference.action.canceled -= FireCanceled;
        weaponManager.ReloadInputActionReference.action.performed -= ReloadPerformed;
        weaponManager.ChangeFireModeActionReference.action.performed -= ChangeFireModePerformed;
    }

    private void Awake()
    {
        weaponManager = WeaponManager.Instance;
        crosshairManager = CrosshairManager.Instance;

        switch (statistics.FireMode)
        {
            case WeaponFireMode.One:
                weaponFireModeStrategy = new OneShotFireModeStrategy(this, statistics);
                break;

            case WeaponFireMode.Three:
                weaponFireModeStrategy = new ThreeShotFireModeStrategy(this, statistics);
                break;

            case WeaponFireMode.Auto:
                weaponFireModeStrategy = new AutoFireModeStrategy(this, statistics);
                break;
        }
    }

    private void Start()
    {
        ammo = statistics.AmmoMagazineSize;
    }

    private void Update()
    {
        weaponFireModeStrategy.Update();

        if (Firerate != 0)
        {
            Firerate -= Time.deltaTime;
        }

        if (isReloading)
        {
            reloadingTime += Time.deltaTime;

            if(reloadingTime > statistics.ReloadTime)
            {
                HandleReload();
                isReloading = false;
                reloadingTime = 0.0f;
            }
        }
    }

    private void FirePerformed(InputAction.CallbackContext ctxt)
    {
        isShooting = true;
    }

    private void FireCanceled(InputAction.CallbackContext ctxt)
    {
        isShooting = false;
    }

    private void ReloadPerformed(InputAction.CallbackContext ctxt)
    {
        if(!isReloading)
        {
            isReloading = true;
        }
    }

    private void ChangeFireModePerformed(InputAction.CallbackContext ctxt)
    {

    }

    private void HandleReload()
    {
        ammo = statistics.AmmoMagazineSize;
    }

    public void Shoot()
    {
        ammo--;

        RaycastHit hit;

        if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f)), out hit, statistics.Distance))
        {
            if(hit.collider)
            {
                crosshairManager.OnHit();
            }
        }     
    }
}
