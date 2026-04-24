using UnityEngine;

public class Excalibur : MonoBehaviour,IGun
{
    public string modename = "Excalibur";
    public string ModeName => modename;

    public void shoot(Transform gunpoint, GunTypeSo Gundata, float finalDamage, bool isCrit, IVisitor extraVisitor = null)
    {
        GameObject bulletObj = Objectpool.Instance.SpawnFromPool("SwordTail", gunpoint.position, gunpoint.rotation);
        if (bulletObj.TryGetComponent<Bullet>(out Bullet bulletScript))
        {


            bulletScript.Setup(finalDamage, isCrit);
            bulletScript.SetOwner(this.gameObject);
            bulletScript.OnobjectSpawn();


        }
    }

}
