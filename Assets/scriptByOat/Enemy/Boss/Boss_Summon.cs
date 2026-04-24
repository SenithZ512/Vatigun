using UnityEngine;

public class Boss_Summon : IBossState
{
    public override void OnEnterState(BossStateManager boss)
    {
        boss.SummonEvent.Invoke();
    }

    public override void OnExitState(BossStateManager boss)
    {
       
    }

    public override void OnUpdateState(BossStateManager boss)
    {
       
    }

   
}
