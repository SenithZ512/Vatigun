using UnityEngine;

public class En_summoner : MonoBehaviour,IAttackBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private float delay;
    private SpawnEnemy Spawncheat;
    [SerializeField] private float count=0;
    private void Start()
    {
        Spawncheat = GetComponent<SpawnEnemy>();
    }
    public void Attack(EnemyStateManager state)
    {
     
    }

   
}
