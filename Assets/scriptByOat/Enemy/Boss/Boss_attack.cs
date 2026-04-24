using UnityEngine;

public class Boss_attack : IBossState
{
    private float timer;
    [SerializeField] private float summonDuration = 2.6f;
    public override void OnEnterState(BossStateManager boss)
    {
        timer = 0;
        boss.anim.SafeSetTrigger("Attack_");
    }

    public override void OnExitState(BossStateManager boss)
    {
       
    }

    public override void OnUpdateState(BossStateManager boss)
    {
        timer += Time.deltaTime;

      
        if (timer >= summonDuration)
        {
            boss.SwitchState(boss._Idle);
        }
    }
}
