using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Magnet : MonoBehaviour
{
    private Transform target;
    private float speed = 20f;
    private bool isAttracting = false; 

    private void Start()
    {
        if (target == null)
        {
            var slot = FindFirstObjectByType<EquimentSlot>();
            if (slot != null) target = slot.transform;
        }
    }

    private void Update()
    {
        if (isAttracting)
        {
            MoveTowardsTarget();
        }
    }

   
    public void MoveTowardsTarget()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            OnReachedTarget();
        }
    }

   
    private void OnReachedTarget()
    {
        isAttracting = false;
        Debug.Log("Item attached!");

     
        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

   
    public void StartAttraction(Transform newTarget = null)
    {
        if (newTarget != null) target = newTarget;
        isAttracting = true;
    }
}
