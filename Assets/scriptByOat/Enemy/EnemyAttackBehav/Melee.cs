using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IElement>(out IElement element))
        {
            DamageVisitor dmgVisitor = new DamageVisitor(damage);
            element.Accept(dmgVisitor);
        }
    }
}
