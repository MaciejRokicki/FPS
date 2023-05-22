public class OneShotFireModeStrategy : WeaponFireModeBaseStrategy
{
    public OneShotFireModeStrategy(Weapon weapon, WeaponStatistics weaponStatistics) : base(weapon, weaponStatistics)
    {
    }

    public override void Update()
    {
        if (weapon.CanShoot)
        {
            weapon.Shoot();

            weapon.Firerate = weaponStatistics.Firerate;
            weapon.IsShooting = false;
        }
    }
}
