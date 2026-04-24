using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float TimeToDissaear = 2f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionDamage = 150f;
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private Collider[] colliders;
    [SerializeField]private LayerMask layerMask;
    private bool hasExploded = false;

    private AudioSource aduip;
    public void Explode()
    {
        aduip = GetComponent <AudioSource>();

        if (hasExploded) return; 
        hasExploded = true;     
        colliders = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);
        HashSet<IElement> damagedElements = new HashSet<IElement>();
        aduip.PlaySoundNow();
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject == gameObject) continue;

           
            if (hit.TryGetComponent<IElement>(out IElement element))
            {
                if (!damagedElements.Contains(element))
                {
                    DamageVisitor dmgVisitor = new DamageVisitor(explosionDamage);
                    element.Accept(dmgVisitor);
                    damagedElements.Add(element);
                }
            }

        
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
              
                if (hit.TryGetComponent<EnemyStateManager>(out EnemyStateManager enemyStateManager))
                {
                    if (enemyStateManager.currentState != enemyStateManager._Death)
                    {
                       
                            enemyStateManager.SwitchState(enemyStateManager._Stun);
                    }
                }

                if (hit.TryGetComponent<BossStateManager>(out BossStateManager boss))
                {
                    if (boss.currentState != boss._death)
                    {

                        boss.SwitchState(boss._stun);
                    }
                }
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 3f);
               
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius); 
    }
    private void OnEnable()
    {
        hasExploded = false;
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
          
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        
        if (TryGetComponent<Collider>(out Collider col))
        {
            col.enabled = true;
        }
        StopAllCoroutines();

        StartCoroutine(Dissapear());
    }
   

    private IEnumerator Dissapear()
    {
       
        yield return new WaitForSeconds(TimeToDissaear);
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        gameObject.SetActive(false);
    }
}
