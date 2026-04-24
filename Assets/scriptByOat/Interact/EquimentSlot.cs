using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class EquimentSlot : MonoBehaviour,IThrow
{
    public List<GameObject> gunList = new List<GameObject>();
    public GameObject HoldingPoint;
    public GameObject CurrentHolding;
    [SerializeField] private float ThrowForce = 5f;
    [SerializeField] private UpdateAmmoUI ammoUI;
    private int currentindex;
    private int previousIndex;
    public HeldStatus held;


    public bool isCritHundredActive = false;
    private void Start()
    {
        held = GetComponent<HeldStatus>();
        while (gunList.Count < 10)
        {
            gunList.Add(null);
        }
    }
    public void AddGun(GameObject newGun)
    {
        Gun gunScript = newGun.GetComponent<Gun>();
        if (gunScript == null || gunScript.guntype == null) return;

        
        int targetIndex = GetTargetIndexBySO(gunScript.guntype);

        if (targetIndex != -1 && targetIndex < gunList.Count)
        {
         
            if (gunList[targetIndex] != null)
            {
                Destroy(gunList[targetIndex]);
            }

           
            gunList[targetIndex] = newGun;

            newGun.transform.SetParent(HoldingPoint.transform);
            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localRotation = Quaternion.identity;

            gunScript.SetupGunSource();
            newGun.SetActive(false);
            if(CurrentHolding == null) OnHoldGun(targetIndex);
            GameEvent.UpdateAmmo?.Invoke();
         
        }

    }
    private void Update()
    {

        for (int i = 0; i < gunList.Count && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                
                if (gunList[i] != null)
                {
                    if (currentindex != i) previousIndex = currentindex;
                    OnHoldGun(i);
                    GameEvent.UpdateAmmo?.Invoke();
                }
                break;
            }
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
           
            int direction = scroll > 0 ? 1 : -1;
            SwitchGunByScroll(direction);
        }
        if (CurrentHolding!= null)
        {
            Gun currentGun = CurrentHolding.GetComponent<Gun>();
          
            if (Input.GetKeyDown(KeyCode.G))
            {
                OnThrow();
                GameEvent.UpdateAmmo?.Invoke();
            }
            if (Input.GetMouseButton(0))
            {
                
                if (currentGun.AllAmmoleft <= 0 && currentGun.currentAmmo <= 0)
                {
                    currentGun.currentAmmo = 0;
                    GameEvent.UpdateAmmo?.Invoke();

                  //  SwitchToNextAvailableGun();
                    return;
                }
                currentGun.GetComponent<Gun>().ExecuteFire();
                GameEvent.UpdateAmmo?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentGun.GetComponent<Gun>().ReloadFuc();
                GameEvent.UpdateAmmo?.Invoke();
                
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwapToPreviousGun();
                GameEvent.UpdateAmmo?.Invoke();
            }
        }
       

    }
    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.TryGetComponent<Gun>(out Gun _gun))
        {
            if (_gun.transform.parent != null) return;
            _gun.gameObject.transform.position = HoldingPoint.transform.position;
            _gun.gameObject.transform.localRotation = HoldingPoint.transform.localRotation;
            _gun.gameObject.transform.SetParent(HoldingPoint.transform);
            _gun.GetComponent<Collider>().enabled = false;

            _gun.GetComponent<Rigidbody>().isKinematic = true;
            AddGun(_gun.gameObject);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IEffectPickUp>(out IEffectPickUp effectPickUp))
        {
            effectPickUp.Onpickup(this);
        }
    }

    public void SwapToPreviousGun()
    {
      
        if (gunList.Count > 1 && previousIndex < gunList.Count)
        {
            int tempIndex = currentindex; 

            OnHoldGun(previousIndex);

            previousIndex = tempIndex; 

           
        }
    }
    private void OnHoldGun(int index)

    {
        if (index < 0 || index >= gunList.Count || gunList[index] == null) return;

        for (int i = 0; i < gunList.Count; i++)
        {
            if (gunList[i] == null) continue;

            if (i == index)
            {
                bool currentState = !gunList[i].activeSelf;
                gunList[i].SetActive(currentState);
                if (currentState)
                {
                    CurrentHolding = gunList[index];
                    currentindex = i;
                }
                else
                {
                    CurrentHolding = null;
                    currentindex = -1;
                    ammoUI.ClearHud();
                }
            }
            else
            {
                gunList[i].SetActive(false);
            }
        }

    }
    private void SwitchToNextAvailableGun()
    {
        if (gunList.Count <= 1)
        {
           
            if (CurrentHolding != null) CurrentHolding.SetActive(false);
            return;
        }

       
        for (int i = 0; i < gunList.Count; i++)
        {
            
            int nextIndex = (currentindex + 1 + i) % gunList.Count;
            Gun nextGun = gunList[nextIndex].GetComponent<Gun>();
            if (nextGun == null) continue ;
            if (nextGun.currentAmmo > 0 || nextGun.AllAmmoleft > 0)
            {
                OnHoldGun(nextIndex);
               
                return; 
            }
        }

        CurrentHolding.SetActive(false);
    }
    public void OnThrow()
    {
        if (CurrentHolding == null) return;

        GameObject thrownGun = CurrentHolding;
        CurrentHolding.transform.SetParent(null);
        CurrentHolding.GetComponent<Gun>().OnThrow();
        CurrentHolding.GetComponent<Rigidbody>().AddForce(HoldingPoint.transform.forward * ThrowForce, ForceMode.Impulse);
        CurrentHolding.GetComponent<Collider>().enabled=true;
        gunList[currentindex] = null;
        CurrentHolding = null;
        currentindex = -1;
        ammoUI.ClearHud();
    }
    public void ActivateCritBuff(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(CritBuffRoutine(duration));
    }

    private IEnumerator CritBuffRoutine(float duration)
    {
        isCritHundredActive = true;
        Debug.Log("CRIT BUFF ACTIVE!");
        yield return new WaitForSeconds(duration);
        isCritHundredActive = false;
        Debug.Log("CRIT BUFF EXPIRED");
    }
    public void NotifyAllGunsUpgrade()
    {
        foreach (GameObject gunObj in gunList)
        {
            if(gunObj == null) continue;
            gunObj.GetComponent<Gun>().RefreshGunStats();
        }
    }
    private int GetTargetIndexBySO(GunTypeSo type)
    {
        
        if (type.GunTypename == "Pistol") return 0;
        if (type.GunTypename == "ShotGun") return 1;
        if (type.GunTypename == "AssaltRifle") return 2;
        if (type.GunTypename == "CrossBow") return 3;
        if (type.GunTypename == "RocketLauncher") return 4;
        if (type.GunTypename == "Sword") return 5;

       
        for (int i = 0; i < gunList.Count; i++)
        {
            if (gunList[i] == null) return i;
        }
        return -1;
    }
    private void SwitchGunByScroll(int direction)
    {
      
        bool hasAnyGun = false;
        foreach (var g in gunList) if (g != null) hasAnyGun = true;
        if (!hasAnyGun) return;

        int nextIdx = currentindex;

       
        for (int i = 0; i < gunList.Count; i++)
        {
         
            nextIdx = (nextIdx + direction + gunList.Count) % gunList.Count;

            if (gunList[nextIdx] != null)
            {
                if (currentindex != nextIdx) previousIndex = currentindex;
                OnHoldGun(nextIdx);
                GameEvent.UpdateAmmo?.Invoke();
                return; 
            }
        }
    }
}
