using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gun : MonoBehaviour, IThrow
{
    public GunTypeSo guntype;
    private IGun mode1;
    private IGun mode2;
    public IGun currentMode;
    private string Type;
    private float FirerateCount;
    [SerializeField] private Transform GunPoint;
    public int currentAmmo;
    public int AllAmmoleft;

    private IGunSource gunSource ;

    private AudioSource audio;
    public AudioClip clip;
    private bool Isreload;
    private void Start()
    {
        Type = guntype.GunTypename;
       
            switch (Type)
        {
            case "AssaltRifle":
                mode1 = GetComponent <ASR_mode1>();
                mode2 = GetComponent<ASR_mode2>();
                currentMode = mode1;
                break;
            case "Pistol":
            
                mode1 = GetComponent<PistolFistMode>();
                mode2 = GetComponent<PistolSeconMode>();
               
                currentMode = mode1;    
                break;
            case "ShotGun":
                mode1 = GetComponent<ShortGunMode1>();
                mode2 = GetComponent<ShortGUnmode2>();
                currentMode = mode1;
             
                break;
            case "CrossBow":
                mode1 = GetComponent<CrossBowMode1>();
                currentMode = new BulletArmorBreaker(mode1);
                break;
            case "RocketLauncher":
                mode1 = GetComponent<RockeLaunderMode>();
                currentMode = mode1;
                break;
            case "BallFire":
                mode1 = GetComponent<EnemyBallFire>();
                currentMode = mode1;
                break;
            case "Sword":
                mode1 = GetComponent<Excalibur>();
                currentMode = mode1;
                break;
                
        }
        currentAmmo = guntype.MaxCapacity;

        audio = GetComponent<AudioSource>();
         AllAmmoleft = guntype.MaxAmmoCanTake;
    }
   
    public void OnThrow()
    {
        gunSource = null;
        GameEvent.OnGunUpgrade -= RefreshGunStats;
        StartCoroutine(Throwed());
    }
    
  
    public void ExecuteFire()
    {
        if (Isreload) return;
        if (Time.time < FirerateCount) return;
        if (currentAmmo <= 0)
        {
            
            if (AllAmmoleft > 0)
            {
                ReloadFuc();
            }
            return;
        }
        float damageToSend = guntype.Damage;
        bool critState = false;
       
        GameObject ownerObj = transform.root.gameObject;

        
        if (ownerObj.CompareTag("Player"))
        {
         
            EquimentSlot playerSlot = ownerObj.GetComponent<EquimentSlot>();
            if (playerSlot != null && playerSlot.isCritHundredActive)
            {
                critState = true;
                damageToSend = guntype.Damage * 3f;
               
            }
        }
        else
        {
           
            critState = false;
            damageToSend = guntype.Damage;
        }

        currentMode.shoot(GunPoint, guntype, damageToSend, critState);
        FirerateCount = Time.time + guntype.FireRate;
        currentAmmo--;
        audio.PlayOneshotNow(clip);



    }
    public void SwitchMode()
    {
        if (mode2 == null) return;
        currentMode = (currentMode == mode1) ? mode2 : mode1;
        GameEvent.UpdateAmmo?.Invoke();

    }
    public void ReloadFuc()
    {
        if (Isreload || currentAmmo == guntype.MaxCapacity || AllAmmoleft <= 0) return;
        StartCoroutine(Reloading());
        
    }
   private IEnumerator Reloading()
    {
        Isreload=true;
        yield return new WaitForSeconds(guntype.ReloadTime);
        int ammoNeeded = guntype.MaxCapacity - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, AllAmmoleft);
        currentAmmo += ammoToReload;
        AllAmmoleft -= ammoToReload;
        
      
        Isreload = false;   
    }
    private IEnumerator Throwed()
    {
      
        gameObject.layer = 7;
        if(gameObject.GetComponent<Rigidbody>() == null )
        {
            gameObject.AddComponent<Rigidbody>();
        }
       
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(2f);
        gameObject.layer = 1;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    public void Addammo(int amount)
    {
       
        int maxLimit = GetEffectiveMaxAmmocantake();

        
        AllAmmoleft = Mathf.Min(AllAmmoleft + amount, maxLimit);

       
        GameEvent.UpdateAmmo?.Invoke();

    }
    public AmmoTypeSO GetAmmoType()
    {
        if (guntype != null)
        {
            return guntype.AmmoType;
        }
        return null;
    }
    public int GetEffectiveMaxAmmocantake()
    {
        return (gunSource != null)
            ? gunSource.GetMaxAmmoCanTake(guntype.MaxAmmoCanTake)
            : guntype.MaxAmmoCanTake;
    }
    public void SetupGunSource()
    {
        
        if (transform.root.CompareTag("Player"))
        {
            gunSource = new PlayerGunAdpater(this.guntype);
         
            AllAmmoleft = GetEffectiveMaxAmmocantake();
          
        }
    }
    public void RefreshGunStats()
    {
        if (gunSource == null) return;
        AllAmmoleft = GetEffectiveMaxAmmocantake();
        GameEvent.UpdateAmmo?.Invoke();
    }
   
}
