using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float force =20;
    [SerializeField] private float radious = 10;
   
  
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
       


    }
    public void explode()
    {
        foreach (Transform child in transform)
        {
         
    

           
            if (!child.TryGetComponent<Magnet>(out Magnet mgch))
            {
                mgch = child.gameObject.AddComponent<Magnet>();
            }

            if (!child.TryGetComponent<Rigidbody>(out Rigidbody childRb))
            {
                childRb = child.gameObject.AddComponent<Rigidbody>();
            }

          
            childRb.velocity = Vector3.zero;
            childRb.angularVelocity = Vector3.zero;
            childRb.useGravity = false; 

        
            childRb.AddExplosionForce(force, transform.position, radious, 0);

           
            mgch.StartAttraction();
        }
      

    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}
