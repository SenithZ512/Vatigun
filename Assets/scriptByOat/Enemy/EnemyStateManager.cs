using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] private string currentStateName;
    public EnemyBaseState currentState;
    public ES_Idle _Idle = new ES_Idle();
    public ES_Chase _Chase = new ES_Chase();
    public ES_Attack _Attack = new ES_Attack();
    public ES_Stun _Stun = new ES_Stun();
    public ES_Death _Death = new ES_Death();

    public RagDollEneable _DollEneable;
  
    [Header("AI Settings")]
    public float chaseRange = 10f;  
    public float attackRange = 2f; 
    public float moveSpeed = 5f;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Transform player;
    [HideInInspector] public Rigidbody rb;

    public IAttackBehaviour[] attackBehaviours;
    private void Awake()
    {
        attackBehaviours = GetComponents<IAttackBehaviour>();
       anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        if (_DollEneable != null)
        {
            //_DollEneable = GetComponentInChildren<RagDollEneable>();
        }
        currentState = _Idle;
    }
    private void Start()
    {
        foreach (var attackBehaviour in attackBehaviours)
        {
            Debug.Log(attackBehaviour.GetType());
        }
        if (_DollEneable != null)
        {
            _DollEneable.EnableAnimator();
        }
        currentState.OnEnterState(this);
    }
    private void Update()
    {
        currentState.OnUpdateState(this);
      
    }
    public void SwitchState(EnemyBaseState state)
    {
        if (currentState != null)
        {
            currentState.OnExitState(this);
        }
        currentState = state;
        state.OnEnterState(this);
        currentStateName = currentState.GetType().Name;
        Debug.Log("in state"+ currentState);
    }
    private void OnCollisionEnter(Collision collision)
    {
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
