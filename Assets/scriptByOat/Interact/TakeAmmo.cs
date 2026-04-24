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
       
    }

    public void Onpickup(EquimentSlot equid)
    {
        if (equid != null) 
        {
            bool wasAnyAmmoAdded = false;
            foreach (GameObject gunObject in equid.gunList)
            {
                if (gunObject == null) continue;
                if (gunObject.TryGetComponent<Gun>(out Gun gunScript))
                {
                    if (gunScript.GetAmmoType() == _ammoType)
                    {
                        // เช็คก่อนว่ากระสุนในปืนกระบอกนี้เต็มหรือยัง
                        int maxAllowed = gunScript.GetEffectiveMaxAmmocantake();
                        if (gunScript.AllAmmoleft < maxAllowed)
                        {
                            gunScript.Addammo(_ammoAmount);
                            wasAnyAmmoAdded = true;

                            if (equid.CurrentHolding == gunObject)
                            {
                                GameEvent.UpdateAmmo?.Invoke();
                            }
                        }
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
