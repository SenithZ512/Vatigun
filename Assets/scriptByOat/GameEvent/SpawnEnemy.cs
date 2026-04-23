using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private string Tagname;
    [SerializeField] private int amount;
    [SerializeField] private float spawndelay =1f;
    private void CallSpawnAarmy()
    {
       StartCoroutine(SpawnAarmy());
    }
    private IEnumerator SpawnAarmy()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPos = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            Objectpool.Instance.SpawnFromPool(Tagname, randomPos, transform.rotation);

            yield return new WaitForSeconds(spawndelay);
        }
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Spawn"))
        {
            CallSpawnAarmy();
        }
    }
}
