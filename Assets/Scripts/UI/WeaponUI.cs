using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    private WeaponManager weaponManager;

    [SerializeField]
    private TextMeshProUGUI ammo;
    [SerializeField]
    private TextMeshProUGUI fireMode;
    [SerializeField]
    private Slider slider;

    private StringBuilder stringBuilder;

    private void Awake()
    {
        weaponManager = WeaponManager.Instance;
        stringBuilder = new StringBuilder();

        weaponManager.OnWeaponChange += OnWeaponChange;
    }

    private void OnWeaponChange(Weapon weapon)
    {
        weapon.OnFireModeChange += OnFireModeChange;
        weapon.OnAmmoChange += OnAmmoChange;
        weapon.OnReloading += OnReloading;

        OnFireModeChange(weapon, weapon.MainFireMode);
        OnAmmoChange(weapon.Ammo, weapon.statistics.AmmoMagazineSize);
    }

    private void OnFireModeChange(Weapon weapon, bool mainFireMode)
    {
        WeaponFireMode fireMode = mainFireMode ? weapon.statistics.FireMode : weapon.statistics.AlternativeFireMode;

        switch (fireMode)
        {
            case WeaponFireMode.One:
                stringBuilder.Append(">");
                break;
            case WeaponFireMode.Three:
                stringBuilder.Append(">>>");
                break;
            case WeaponFireMode.Auto:
                stringBuilder.Append("auto");
                break;
        }

        if(weapon.statistics.FireMode != weapon.statistics.AlternativeFireMode)
        {
            stringBuilder.Append(" (");

            switch (!mainFireMode ? weapon.statistics.FireMode : weapon.statistics.AlternativeFireMode)
            {
                case WeaponFireMode.One:
                    stringBuilder.Append(">");
                    break;
                case WeaponFireMode.Three:
                    stringBuilder.Append(">>>");
                    break;
                case WeaponFireMode.Auto:
                    stringBuilder.Append("auto");
                    break;
            }

            stringBuilder.Append(")");
        }

        this.fireMode.text = stringBuilder.ToString();
        stringBuilder.Clear();
    }

    private void OnAmmoChange(int ammo, int maxAmmo)
    {
        stringBuilder
            .Append(ammo)
            .Append("/")
            .Append(maxAmmo);

        this.ammo.text = stringBuilder.ToString();

        stringBuilder.Clear();
    }

    private void OnReloading(float time, float targetTime)
    {
        if(time == 0)
        {
            slider.gameObject.SetActive(false);
            return;
        }
        else
        {
            if(!slider.gameObject.activeSelf)
            {
                slider.gameObject.SetActive(true);
            }

            slider.value = time;
            slider.maxValue = targetTime;
        }
    }
}
