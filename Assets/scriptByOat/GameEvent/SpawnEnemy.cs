using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [System.Serializable]
    public class Stack
    {
        public string  Tagname;
        public int amount;
        public float spawndelay;
    }
    public List<Stack> stacks;
    public Dictionary<string, Queue<GameObject>> StackDic;

    public void CallSpawnAarmy()
    {
       StartCoroutine(SpawnAarmy());
    }
   
    private IEnumerator SpawnAarmy()
    {
        foreach (Stack stack in stacks)
        {
           
            for (int i = 0; i < stack.amount; i++)
            {
                Vector3 randomPos = transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                Objectpool.Instance.SpawnFromPool(stack.Tagname, randomPos, transform.rotation);
                yield return new WaitForSeconds(stack.spawndelay);
            }

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
