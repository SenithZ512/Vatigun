using UnityEngine;
using UnityEngine.UIElements;

public class ExctueGunAnim : MonoBehaviour
{
    private Gun gun;
    private AimatPlayer aim;
    void Start()
    {
        aim = transform.root.GetComponentInChildren<AimatPlayer>();
        gun = transform.root.GetComponentInChildren<Gun>();
    }

    public void ExcuteGun()
    {
        aim.Aim();
        gun.ExecuteFire();
       
        gun.currentAmmo += 1;
        Debug.Log("Fire");
    }

 
}
