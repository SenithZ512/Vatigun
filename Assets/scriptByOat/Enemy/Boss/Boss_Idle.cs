using UnityEngine;

public class Boss_Idle : IBossState
{
    private float count;

    private float random;
    public override void OnEnterState(BossStateManager boss)
    {
       random = Random.Range(3f,6f);
    }

    public override void OnExitState(BossStateManager boss)
    {
       
    }

    public override void OnUpdateState(BossStateManager boss)
    {
        count+=1 * Time.deltaTime;

        if (count < random) return;
        boss.SwitchState(boss._Summon);
        
        
    }
}
