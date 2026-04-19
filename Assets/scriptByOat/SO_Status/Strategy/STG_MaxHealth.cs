using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STG_MaxHealth : MonoBehaviour, IEffectPickUp
{

    [SerializeField] private float Healtdrop=40f;
    [SerializeField] private float armordrop=40f;
    [SerializeField] private bool IsmaxHealth;
    public void Onpickup(EquimentSlot equid)
    {
        var source = equid.held._statusSource;
        float maxH = (source != null) ? source.GetHealth() : equid.held.status.Health;
        float maxA = (source != null) ? source.GetArmor() : equid.held.status.Armor;
        if (IsmaxHealth)
        {
            float finalhealth = equid.held.status.Health - equid.held._health;
            float finalarmor = equid.held.status.Armor - equid.held._armor;

            equid.held._health = maxH;
            equid.held._armor = maxA;

        }
        else
        {

            equid.held._health += Healtdrop;
            equid.held._armor += armordrop;
        }
        if (equid.held._health > maxH) equid.held._health = maxH;
        if (equid.held._armor > maxA) equid.held._armor = maxA;

        GameEvent.UpdatePLayerStatus?.Invoke();
    }
}
