using UnityEngine;

public abstract class WeaponFireModeBaseStrategy
{
    protected Weapon weapon;
    protected WeaponStatistics weaponStatistics;

    public WeaponFireModeBaseStrategy(Weapon weapon, WeaponStatistics weaponStatistics)
    {
        this.weapon = weapon;
        this.weaponStatistics = weaponStatistics;
    }

    public abstract void Update();
}
