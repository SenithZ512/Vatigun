using UnityEngine;

public class PlayerGunAdpater : IGunSource
{
    private GunTypeSo _type;
    public PlayerGunAdpater(GunTypeSo type)
    {
        _type = type;
    }
    public int GetMaxAmmoCanTake(int baseMaxAmmo)
    {
        return baseMaxAmmo + (UpgradeData.AmmoUpgradeLevel * _type.AmmoBonusPerLevel);
    }
}
