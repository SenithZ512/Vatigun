using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOndeath : MonoBehaviour, IOndeath
{
    private EnemyStateManager stateManager;
    public GameObject Drop;
    private void Start()
    {
        stateManager = GetComponent<EnemyStateManager>();
        Drop.SetActive(false);
    }
    public void OnDeath()
    {
        gameObject.layer = 10;
        stateManager.SwitchState(stateManager._Death);
        Drop.SetActive(true);
        Drop.TryGetComponent<DropItem>(out DropItem dropItem);
        dropItem.explode();
      
    }
}
