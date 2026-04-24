using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STG_FillAmmo : MonoBehaviour, IEffectPickUp
{
    public void Onpickup(EquimentSlot equid)
    {
        foreach (GameObject gunObj in equid.gunList)
        {
            if(gunObj==null) continue;  
            Gun gunScript = gunObj.GetComponent<Gun>();

            if (gunScript != null)
            {

                gunScript.AllAmmoleft = gunScript.GetEffectiveMaxAmmocantake();

                GameEvent.UpdateAmmo?.Invoke();
            }
            Destroy(gameObject);
            
        }

    }
}
