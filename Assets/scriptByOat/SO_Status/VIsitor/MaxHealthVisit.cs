using UnityEngine;

public class MaxHealthVisit : MonoBehaviour,IVisitor
{
    private float _hAdd;
    private float _aAdd;
    public void Visit(HeldStatus heldstatus)
    {
        heldstatus._health += _hAdd;
        heldstatus._armor += _aAdd;
        float maxH = heldstatus._statusSource.GetHealth();
        float maxA = heldstatus._statusSource.GetArmor();
        if (heldstatus._health > maxH) heldstatus._health = maxH;
        if (heldstatus._armor > maxA) heldstatus._armor = maxA;
    }
    public MaxHealthVisit(float h, float a)
    {
        _hAdd = h;
        _aAdd = a;
    }

}
