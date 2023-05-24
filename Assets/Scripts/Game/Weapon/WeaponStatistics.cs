using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatistics", menuName = "Weapons/Statistics")]
public class WeaponStatistics : ScriptableObject
{
    public InteractiveObjectMaterial ObjectMaterial;
    public WeaponFireMode FireMode;
    public WeaponFireMode AlternativeFireMode;
    public float Damage;
    public int AmmoMagazineSize;
    public float Firerate;
    public float ReloadTime;
    public float Distance;
}
