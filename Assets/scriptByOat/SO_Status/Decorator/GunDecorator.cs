using UnityEngine;

public abstract class GunDecorator : IGun
{
    protected IGun decoratedGun;
    public GunDecorator(IGun gun) { this.decoratedGun = gun; }
    public string ModeName => decoratedGun.ModeName;

    public virtual void shoot(Transform gunpoint, GunTypeSo Gundata, float finalDamage, bool isCrit, IVisitor extraVisitor = null)
    {
        if (decoratedGun != null)
        {
            decoratedGun.shoot(gunpoint, Gundata, finalDamage, isCrit, extraVisitor);
        }
    }

   
}
