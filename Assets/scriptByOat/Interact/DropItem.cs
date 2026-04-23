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
    private void explode()
    {
        foreach (Transform child in transform)
        {
            Rigidbody childRb = child.gameObject.GetComponent<Rigidbody>();

            if (childRb == null)
            {
                childRb = child.gameObject.AddComponent<Rigidbody>();
            }
            childRb.AddExplosionForce(force, transform.position, radious, 0);

        }


    }
    private void OnGUI()
    {
       // if (GUILayout.Button("Explode"))
        //{
       //     explode();
      //  }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}
