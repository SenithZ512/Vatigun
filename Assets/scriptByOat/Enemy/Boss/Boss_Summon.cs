using Unity.Burst.Intrinsics;
using UnityEngine;

public class Boss_Summon : IBossState
{
    private float timer;
    [SerializeField] private float summonDuration = 4.56f;
    public override void OnEnterState(BossStateManager boss)
    {
        timer = 0;
        boss.anim.SafeSetTrigger("Summon");
     
        boss.SummonEvent.Invoke();
       

    }

    public override void OnExitState(BossStateManager boss)
    {
       
    }

    public override void OnUpdateState(BossStateManager boss)
    {
        timer += Time.deltaTime;

        // เมื่อเวลาผ่านไปจนครบกำหนด ให้เปลี่ยนกลับไป Idle
        if (timer >= summonDuration)
        {
            boss.SwitchState(boss._Idle);
        }
    }

   
}
