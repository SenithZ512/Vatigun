using UnityEngine;

public class ShieldBreak : MonoBehaviour,IVisitor
{
    private float Dmg;
    public ShieldBreak(float amount)
    {
        Dmg = amount;
    }
    public void Visit(HeldStatus heldstatus)
    {
        if (heldstatus._armor <=0 ) return;
       heldstatus._armor -= Dmg;

        if(heldstatus._armor <=0 ) heldstatus._armor = 0;
    }

   
}
