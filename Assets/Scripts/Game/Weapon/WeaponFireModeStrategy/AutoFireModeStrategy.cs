public class AutoFireModeStrategy : WeaponFireModeBaseStrategy
{
    public AutoFireModeStrategy(Weapon weapon, WeaponStatistics weaponStatistics) : base(weapon, weaponStatistics)
    {
    }

    public override void Update()
    {
        if (weapon.CanShoot)
        {
            weapon.Shoot();

            weapon.Firerate = weaponStatistics.Firerate;
        }
    }
}
