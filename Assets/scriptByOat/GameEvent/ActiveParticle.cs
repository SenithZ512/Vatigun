using System.Collections.Generic;
using UnityEngine;

public class ActiveParticle : MonoBehaviour
{
   
    [SerializeField]private List<GameObject> particlesobj = new List<GameObject>();
    private void OnEnable()
    {
      foreach (GameObject obj in particlesobj)
        {
            obj.GetComponent<ParticleSystem>().Play();
        }

      
    }
    private void OnDisable()
    {
        foreach (Transform t in transform)
        {
            ParticleSystem practice = t.GetComponent<ParticleSystem>();
            if (practice != null)
            {
                practice.Stop();
                particlesobj.Add(practice.gameObject);
            }

        }
    }
}
