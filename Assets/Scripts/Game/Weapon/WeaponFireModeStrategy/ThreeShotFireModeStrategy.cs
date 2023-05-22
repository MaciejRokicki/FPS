using System.Threading.Tasks;

public class ThreeShotFireModeStrategy : WeaponFireModeBaseStrategy
{
    public ThreeShotFireModeStrategy(Weapon weapon, WeaponStatistics weaponStatistics) : base(weapon, weaponStatistics)
    {
    }

    public override void Update()
    {
        if (weapon.CanShoot)
        {
            HandleThreeShots().ConfigureAwait(false);
            weapon.IsShooting = false;
        }
    }

    private async Task HandleThreeShots()
    {
        for (int i = 0; i < 3; i++)
        {
            if (weapon.Ammo > 0)
            {
                weapon.Shoot();
            }
            else
            {
                break;
            }

            await Task.Delay(100);
        }
    }
}
