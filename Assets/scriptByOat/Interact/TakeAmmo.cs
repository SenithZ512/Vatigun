using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAmmo : MonoBehaviour, IAmmo,IEffectPickUp
{
    [SerializeField] private AmmoTypeSO _ammoType;
    [SerializeField] private int _ammoAmount;
    public AmmoTypeSO AmmoType => _ammoType;

    public int AmmoAmount { get => _ammoAmount; set => _ammoAmount = value; }
   
    public void TakeTheAmmo()
    {
        Destroy(gameObject);
    }

    public void Onpickup(EquimentSlot equid)
    {
        if (equid != null) 
        {
            bool wasAmmoAdded = false;
            foreach (GameObject gunObject in equid.gunList)
            {
                if (gunObject == null) continue;
                if (gunObject.TryGetComponent<Gun>(out Gun gunScript))
                {
                  
                    if (gunScript.GetAmmoType() == _ammoType)
                    {
                        gunScript.Addammo(_ammoAmount);
                        wasAmmoAdded = true;
                        if (equid.CurrentHolding == gunObject)
                        {
                            GameEvent.UpdateAmmo?.Invoke();
                        }
                    }
                }
            }
            if (wasAmmoAdded) TakeTheAmmo();
        }
    }
}
