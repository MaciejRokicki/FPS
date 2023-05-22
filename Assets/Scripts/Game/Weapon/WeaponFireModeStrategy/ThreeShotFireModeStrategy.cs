public class ThreeShotFireModeStrategy : WeaponFireModeBaseStrategy
{
    public ThreeShotFireModeStrategy(Weapon weapon, WeaponStatistics weaponStatistics) : base(weapon, weaponStatistics)
    {
    }

    public override void Update()
    {
        if (weapon.CanShoot)
        {
            weapon.Shoot();
            weapon.Shoot();
            weapon.Shoot();

            weapon.Firerate = weaponStatistics.Firerate;
            weapon.IsShooting = false;
        }
    }
}
