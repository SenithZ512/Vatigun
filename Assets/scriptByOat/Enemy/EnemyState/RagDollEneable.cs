using UnityEngine;

public class RagDollEneable : MonoBehaviour
{
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private Transform RagdollRoot;
    [SerializeField]
    private bool StartRagdoll = false;

    private Rigidbody[] Rigidbodies;
    private CharacterJoint[] Joints;
    private Collider[] Colliders;

    public Rigidbody Hipbody;
    private void Awake()
    {
        Rigidbodies = RagdollRoot.GetComponentsInChildren<Rigidbody>();
        Joints = RagdollRoot.GetComponentsInChildren<CharacterJoint>();
        Colliders = RagdollRoot.GetComponentsInChildren<Collider>();

        if (StartRagdoll)
        {
            EnableRagdoll();
        }
        else
        {
            EnableAnimator();
        }
    }

    public void EnableRagdoll()
    {
       
        Animator.enabled = false;
       //if (GetComponentInParent<CapsuleCollider>() != null)
       //    GetComponentInParent<CapsuleCollider>().enabled = false;
        foreach (CharacterJoint joint in Joints)
        {
            joint.enableCollision = false;
        }
        foreach (Collider collider in Colliders)
        {
            collider.enabled = true;
        }
       
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.detectCollisions = true;
            rigidbody.useGravity = true;
        }
    }

    public void EnableAnimator()
    {
    
        Animator.enabled = true;
     // if (GetComponentInParent<CapsuleCollider>() != null)
      //    GetComponentInParent<CapsuleCollider>().enabled = true;
        foreach (CharacterJoint joint in Joints)
        {
            joint.enableCollision = false;
        }
        foreach (Collider collider in Colliders)
        {
            if (collider.gameObject.GetComponent<Melee>()||
     collider.gameObject.name.Contains("DamgaeHtibox")) continue;
            collider.enabled = false;
        }
      
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
        }
    }
    public void EnableRagdollDeath()
    {

        Animator.enabled = false;
        //if (GetComponentInParent<CapsuleCollider>() != null)
        //    GetComponentInParent<CapsuleCollider>().enabled = false;
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.isKinematic = false;
           
            rigidbody.detectCollisions = true;
            rigidbody.useGravity = true;
        }
        foreach (CharacterJoint joint in Joints)
        {
            joint.enableCollision = false;
        }
        foreach (Collider collider in Colliders)
        {
            collider.enabled = true;
            
        }

       
    }

}
