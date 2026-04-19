using UnityEngine;

public class BulletArmorBreaker : GunDecorator
{
    public BulletArmorBreaker(IGun gun) : base(gun)
    {
     
    }
    public override void shoot(Transform gunpoint, GunTypeSo Gundata, float finalDamage, bool isCrit, IVisitor extraVisitor = null)
    {
        float piercePercent =30;

        IVisitor armorBreaker = new ShieldBreak(15);

      
        base.shoot(gunpoint, Gundata, finalDamage, isCrit, armorBreaker);

    }
}
