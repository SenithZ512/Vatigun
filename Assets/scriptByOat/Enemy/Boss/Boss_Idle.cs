using UnityEngine;

public class Boss_Idle : IBossState
{
    private float count;

    private float random;
    public override void OnEnterState(BossStateManager boss)
    {
        count = 0;
        boss.anim.SafeSetTrigger("IsIdle");
       random = Random.Range(3f,6f);
       
    }

    public override void OnExitState(BossStateManager boss)
    {
       
    }

    public override void OnUpdateState(BossStateManager boss)
    {
        count+=1 * Time.deltaTime;

        if (count < random) return;

        if(boss.timecount == 2)
        {
            boss.SwitchState(boss._Summon);
            boss.timecount = 0;
        }
        else
        {
            boss.SwitchState(boss._Shoot);
        }
      
      

    }
}
