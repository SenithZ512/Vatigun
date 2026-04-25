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
                // คำนวณจุดเกิดแบบสุ่มรอบๆ จุดสปอน
                Vector3 randomPos = transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

                // ใช้ randomPos แทน gameObject.transform.position
                GameObject enemy = Objectpool.Instance.SpawnFromPool(stack.Tagname, randomPos, transform.rotation);

                // สำคัญมาก: ถ้าศัตรูมี NavMeshAgent ต้องสั่ง Warp ไปที่ตำแหน่งที่ถูกต้อง
                var agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (agent != null)
                {
                    agent.Warp(randomPos);
                }

                yield return new WaitForSeconds(stack.spawndelay);
            }

        }
    }
   
   
}
