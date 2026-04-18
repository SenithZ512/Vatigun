using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingIdle : MonoBehaviour, IEnemyIdleBehave
{
    public float TargetHeight = 10f;
    public float hoverForce = 15f;
    Ray ray;
    RaycastHit hit;
  
  
    public void OnIdle(EnemyStateManager state)
    {
        state.agent.enabled = true;
        if (state.agent.enabled)
        {
            state.agent.enabled = false;
            state.rb.linearVelocity = Vector3.zero; 
            state.rb.angularVelocity = Vector3.zero; 
        }

        ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out hit, TargetHeight))
        {
            float currentDistance = Vector3.Distance(transform.position, hit.point);

            if (currentDistance < TargetHeight)
            {
                float forceMagnitude = (TargetHeight - currentDistance) * hoverForce;
                state.rb.AddForce(Vector3.up * forceMagnitude, ForceMode.Acceleration);
                if (state.rb.linearVelocity.y > 5f)
                {
                    state.rb.linearVelocity = new Vector3(state.rb.linearVelocity.x, 5f, state.rb.linearVelocity.z);
                }
            }
            else if (currentDistance > TargetHeight + 0.5f)
            {

                state.rb.AddForce(Vector3.down * hoverForce * 0.5f, ForceMode.Acceleration);
               
            }
          
        }
        Vector3 horizontalVel = state.rb.linearVelocity;
        horizontalVel.x = Mathf.Lerp(horizontalVel.x, 0, Time.deltaTime * 5f);
        horizontalVel.z = Mathf.Lerp(horizontalVel.z, 0, Time.deltaTime * 5f);
        state.rb.linearVelocity = new Vector3(horizontalVel.x, state.rb.linearVelocity.y, horizontalVel.z);
        Debug.DrawRay(ray.origin, ray.direction * TargetHeight, Color.red);
    }
}
