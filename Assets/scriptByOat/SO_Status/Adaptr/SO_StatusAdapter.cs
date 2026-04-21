using UnityEngine;

public class SO_StatusAdapter : IStatusSource
{
    private SO_Status baseSo;
    public SO_StatusAdapter(SO_Status so) => baseSo = so;
    public float GetArmor()
    {
        return baseSo.Armor;
    }

    public float GetHealth()
    {
        return baseSo.Health;
    }

   
}
