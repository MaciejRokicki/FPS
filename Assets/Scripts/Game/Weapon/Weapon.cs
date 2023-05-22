using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem muzzleFlashEffect;

    private WeaponManager weaponManager;
    private CrosshairManager crosshairManager;

    [SerializeField]
    private WeaponStatistics statistics;

    private WeaponFireModeBaseStrategy weaponFireModeStrategy;

    private bool mainFireMode = true;

    private int ammo;
    public int Ammo
    {
        get { return ammo; }
        private set { ammo = value; }
    }
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
            if(!isReloading && IsShooting && Firerate == 0 && Ammo > 0)
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

        SetFireMode(statistics.FireMode);
    }

    private void Start()
    {
        Ammo = statistics.AmmoMagazineSize;
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
        if(statistics.FireMode != statistics.AlternativeFireMode)
        {
            HandleChangeFireMode();
        }
    }

    private void HandleReload()
    {
        Ammo = statistics.AmmoMagazineSize;
    }

    private void HandleChangeFireMode()
    {
        mainFireMode = !mainFireMode;

        WeaponFireMode fireMode = mainFireMode ? statistics.FireMode : statistics.AlternativeFireMode;
        SetFireMode(fireMode);
    }

    private void SetFireMode(WeaponFireMode fireMode)
    {
        switch (fireMode)
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

    public void Shoot()
    {
        Ammo--;
        muzzleFlashEffect.Play();

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
