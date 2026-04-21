using UnityEngine;
using UnityEngine.UI;

public class Upgradeitem : MonoBehaviour
{
    public enum UpgradeType { Health, Armor, Ammo ,ChangeMode}
    [SerializeField] private UpgradeType type;

    [SerializeField] private int cost = 200;
    [SerializeField] private float step = 10f;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private EquimentSlot slot;

    private void OnEnable()
    {
        slot = FindFirstObjectByType<EquimentSlot>();
       
        RefreshButton();
    }

    public void RefreshButton()
    {
        if (upgradeButton != null)
        {

            upgradeButton.interactable = Currency.currentcurrency >= cost;
        }
    }

    public void ExecuteUpgrade()
    {
        if (Currency.currentcurrency >= cost)
        {
         

            Currency.currentcurrency -= cost;


            switch (type)
            {
                case UpgradeType.Health:
                    UpgradeData.BonusHealth += step;
                    ApplyEffect(step, 0);
                    break;
                case UpgradeType.Armor:
                    UpgradeData.BonusArmor += step;
                    ApplyEffect(0, step);
                    break;
                case UpgradeType.Ammo:
                    UpgradeData.AmmoUpgradeLevel += 1;
                    UpgradeData.BonusMaxAmmoReserve += 20;
                    GameEvent.OnGunUpgrade?.Invoke();
                    break;
                case UpgradeType.ChangeMode:
                    slot.CurrentHolding.GetComponent<Gun>().SwitchMode();
                    cost = 0;
                    break;
            }


            RefreshButton();
            GameEvent.UpdatePLayerStatus?.Invoke();
            GameEvent.OnUpgrade?.Invoke();

        }
    }
    
    private void ApplyEffect(float h, float a)
    {
        if (slot != null && slot.held != null)
        {
            MaxHealthVisit upgradeEffect = new MaxHealthVisit(h, a);
            slot.GetComponent<HeldStatus>().Accept(upgradeEffect);
        }
    }

}