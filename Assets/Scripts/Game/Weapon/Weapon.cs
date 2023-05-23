using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem muzzleFlashEffect;

    private WeaponManager weaponManager;
    private CrosshairManager crosshairManager;

    public WeaponStatistics statistics;

    private WeaponFireModeBaseStrategy weaponFireModeStrategy;

    private bool mainFireMode;
    public bool MainFireMode
    {
        get
        {
            if (statistics.FireMode == statistics.AlternativeFireMode)
                return true;

            return mainFireMode;
        }
        set
        {
            mainFireMode = value;
        }
    }

    private int ammo;
    public int Ammo
    {
        get { return ammo; }
        private set 
        { 
            ammo = value;
            OnAmmoChange(ammo, statistics.AmmoMagazineSize);
        }
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

    public delegate void AmmoChangeCallback(int ammo, int maxAmmo);
    public event AmmoChangeCallback OnAmmoChange = delegate { };
    public delegate void FireModeChangeCallback(Weapon weapon, bool mainFireMode);
    public event FireModeChangeCallback OnFireModeChange = delegate { };
    public delegate void ReloadingCallback(float time, float targetTime);
    public event ReloadingCallback OnReloading = delegate { };

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

        OnAmmoChange = delegate { };
        OnFireModeChange = delegate { };
        OnReloading = delegate { };
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

            OnReloading(reloadingTime, statistics.ReloadTime);

            if(reloadingTime > statistics.ReloadTime)
            {
                HandleReload();
                isReloading = false;
                reloadingTime = 0.0f;
                OnReloading(reloadingTime, statistics.ReloadTime);
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
        if(!isReloading && Ammo < statistics.AmmoMagazineSize)
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
        OnAmmoChange(Ammo, statistics.AmmoMagazineSize);
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

        OnFireModeChange(this, MainFireMode);
    }

    public void Shoot()
    {
        Ammo--;
        muzzleFlashEffect.Play();
        OnAmmoChange(Ammo, statistics.AmmoMagazineSize);

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
