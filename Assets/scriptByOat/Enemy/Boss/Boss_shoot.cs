using System.Threading;
using UnityEngine;

public class Boss_shoot : IBossState
{
    private float timer;
    [SerializeField] private float summonDuration = 4f;
    public override void OnEnterState(BossStateManager boss)
    {
        boss.timecount++;
        timer = 0; 
        boss.anim.SafeSetTrigger("IsShoot");
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
