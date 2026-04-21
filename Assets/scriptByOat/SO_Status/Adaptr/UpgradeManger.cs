using UnityEngine;
using UnityEngine.UI;

public class UpgradeManger : MonoBehaviour
{
    [SerializeField]private Upgradeitem[] allItems;

    private void Awake()
    {
        
        allItems = GetComponentsInChildren<Upgradeitem>();
    }

    private void OnEnable()
    {
       GameEvent.OnUpgrade+= RefreshAllButtons;
    }
    private void OnDisable()
    {
        GameEvent.OnUpgrade -= RefreshAllButtons;
    }
    public void RefreshAllButtons()
    {
        foreach (var item in allItems)
        {
            item.RefreshButton();
        }
    }
    public void rest()
    {
        UpgradeData.BonusHealth = 0;
        UpgradeData.BonusArmor = 0;
        UpgradeData.BonusMaxAmmoReserve = 0;
        UpgradeData.AmmoUpgradeLevel = 0;
        Currency.currentcurrency = 1000;

      
        if (allItems.Length > 0)
        {
           
            HeldStatus playerStatus = FindFirstObjectByType<EquimentSlot>().GetComponent<HeldStatus>();
            if (playerStatus != null)
            {
                
                MaxHealthVisit resetEffect = new MaxHealthVisit(0, 0);
                playerStatus.Accept(resetEffect);
            }
        }

      
        GameEvent.UpdatePLayerStatus?.Invoke();
        GameEvent.OnUpgrade?.Invoke();
        GameEvent.OnGunUpgrade?.Invoke(); 
        RefreshAllButtons();
    }
}
