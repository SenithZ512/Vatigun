using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IpoolObject 
{
    [SerializeField] private GunTypeSo gunTypeSo;
    [SerializeField] private float speed;
    private string Explode = "Explode";
    [SerializeField] private float damage;
    [SerializeField] private Transform Firepoint;
    private Rigidbody rb;
    private IVisitor _extraVisitor;
    private int _ownerLayer;
    private bool _isCrit;
    public void Setup(float dmg, bool crit)
    {
        damage = dmg;
        _isCrit = crit;
    }
    public void SetOwner(GameObject obj)
    {
        _ownerLayer = obj.transform.root.gameObject.layer;
    }

   
   
   
    private void OnTriggerEnter(Collider collision)
    {
      
        if (collision.CompareTag("Bullet")) return;
        if (collision.gameObject.layer == _ownerLayer) return;
        if (gunTypeSo.GunTypename == "RocketLauncher")
        {
            Objectpool.Instance.SpawnFromPool(Explode, transform.position, transform.rotation);
            gameObject.SetActive(false);
            return;
        }
        /*  float finalDamage = damage;
          if (gameObject.CompareTag("Bullet") && Critmanager.PlayerCritActive)
          {
              finalDamage = damage +damage * 2f; 
          }*/

        if (collision.gameObject.TryGetComponent<IElement>(out IElement _damage))
        {
            //Objectpool.Instance.SpawnFromPool("Blood", transform.position, transform.rotation);
            //Objectpool.Instance.SpawnFromPool("BloodSplash", collision.gameObject.transform.forward, collision.gameObject.transform.rotation);
            DamageVisitor DmgVistit = new DamageVisitor(damage);
            _damage.Accept(DmgVistit);
            gameObject.SetActive(false);
            //if (_isCrit) Debug.Log("BOOM! CRITICAL HIT!");
            if (_extraVisitor != null)
            {
                _damage.Accept(_extraVisitor);
              
            }
        }
        gameObject.SetActive(false);
    }
    public void SetExtraVisitor(IVisitor visitor)
    {
        _extraVisitor = visitor;
    }
    public void OnobjectSpawn()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        StartCoroutine(dissaper());
       
    }
    IEnumerator dissaper()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
