using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisitor : IVisitor
{
    private float _dmgamount;
    private Vector3 _hitPoint;
    public DamageVisitor (float amount)
    {
        _dmgamount = amount;
       
    }
    public DamageVisitor(float amount, Vector3 hitPoint)
    {
        _dmgamount = amount;
        _hitPoint = hitPoint;

    }
    public void Visit(HeldStatus heldstatus)
    {
       

        if (heldstatus._armor > 0)
        {
            if (heldstatus._armor >= _dmgamount)
            {
               
                heldstatus._armor -= _dmgamount;
            }
            else
            {
               
                float remainingDamage = _dmgamount - heldstatus._armor;
                heldstatus._armor = 0;
                heldstatus._health -= remainingDamage;
            }
        }
        else
        {
            
            heldstatus._health -= _dmgamount;
        }
        Vector3 spawnPos = heldstatus.transform.position + (heldstatus.transform.forward * 0.5f);
        Objectpool.Instance.SpawnFromPool("BloodSplash", spawnPos, heldstatus.transform.rotation);
        heldstatus.auido.PlayOneshotNow(heldstatus.clip);
        GameEvent.UpdatePLayerStatus?.Invoke();
        //Debug.Log("Healt " + heldstatus._health+"armor "+ heldstatus._armor);
    }
   
}
