using UnityEngine;

public class Boss_stun : IBossState
{
    private float timer;
    [SerializeField] private float summonDuration = 2f;
   
    public override void OnEnterState(BossStateManager boss)
    {
        timer = 0;
        boss.anim.SafeSetTrigger("Istun");
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
