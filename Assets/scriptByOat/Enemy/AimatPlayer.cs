using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimatPlayer : MonoBehaviour
{
    public Transform m_Player;
    private void Start()
    {
        if(m_Player == null)
        {
            m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
     
    }
    public void Aim()
    {
        transform.LookAt(m_Player.position);
    }
}
