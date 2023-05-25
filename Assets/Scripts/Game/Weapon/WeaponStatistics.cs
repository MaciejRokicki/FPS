using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatistics", menuName = "Weapons/Statistics")]
public class WeaponStatistics : ScriptableObject
{
    public InteractiveObjectMaterial ObjectMaterial;
    public WeaponFireMode FireMode;
    public WeaponFireMode AlternativeFireMode;
    public float Damage;
    public int ShotsNumber;
    public float BulletSpeed;
    public float ShootSpreadRadius;
    public int AmmoMagazineSize;
    public float Firerate;
    public float ReloadTime;
    public float Distance;
}
