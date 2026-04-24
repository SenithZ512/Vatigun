using UnityEngine;

public class OnBossDeath : MonoBehaviour, IOndeath
{
    private BossStateManager stateManager;
    private void Start()
    {
        stateManager = GetComponent<BossStateManager>();
    }
    public void OnDeath()
    {
        stateManager.SwitchState(stateManager._death);
    }
}
