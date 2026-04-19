using UnityEngine;

public class PlayerUpgradeAdapter : IStatusSource
{
    private SO_Status _baseSo;
    public PlayerUpgradeAdapter(SO_Status baseSo) => _baseSo = baseSo;
    public float GetArmor()
    {
      return _baseSo.Armor + UpgradeData.BonusArmor;
    }

    public float GetHealth()
    {
        return _baseSo.Health + UpgradeData.BonusHealth;

    }

   
}
