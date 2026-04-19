using UnityEngine;

public class UpgradeManger : MonoBehaviour
{
    public void AddHealthUpgrade()
    {
        UpgradeData.BonusHealth += 10f;
        GameEvent.UpdatePLayerStatus?.Invoke();
    }
}
