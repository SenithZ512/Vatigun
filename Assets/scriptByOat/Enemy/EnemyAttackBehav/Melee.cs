using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private float damage;
   
    private void OnTriggerEnter(Collider other)
    {
        int myRootLayer = transform.root.gameObject.layer;
        int targetRootLayer = other.transform.root.gameObject.layer;
        if (myRootLayer == targetRootLayer) return;

        if (other.TryGetComponent<IElement>(out IElement element))
        {
            DamageVisitor dmgVisitor = new DamageVisitor(damage);
            element.Accept(dmgVisitor);
        }
    }

   
}
