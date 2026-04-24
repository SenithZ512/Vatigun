using Unity.VisualScripting;
using UnityEngine;

public abstract class IBossState
{
    public abstract void OnEnterState(BossStateManager boss);

    public abstract void OnUpdateState(BossStateManager boss);

    public abstract void OnExitState(BossStateManager boss);
   
}
