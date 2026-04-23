using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOndeath : MonoBehaviour, IOndeath
{
    private EnemyStateManager stateManager;

    private void Start()
    {
        stateManager = GetComponent<EnemyStateManager>();
    }
    public void OnDeath()
    {
        gameObject.layer = 10;
        stateManager.SwitchState(stateManager._Death);
      
    }
}
