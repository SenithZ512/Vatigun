using UnityEngine;
using System.Collections;
public class Boss_death : IBossState
{
    public override void OnEnterState(BossStateManager boss)
    {
        if (boss._DollEneable != null)
        {

            boss._DollEneable.EnableRagdollDeath();
        }
        else
        {
        }
        boss.StartCoroutine(Dissaperar(boss));
    }

    public override void OnExitState(BossStateManager boss)
    {

    }

    public override void OnUpdateState(BossStateManager boss)
    {
   
    }
    private  IEnumerator Dissaperar(BossStateManager boss)
    {
        yield return new WaitForSeconds(3f);
        boss.gameObject.SetActive(false);
    }
}
