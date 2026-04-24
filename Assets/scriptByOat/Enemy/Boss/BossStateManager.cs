using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BossStateManager : MonoBehaviour
{
    [SerializeField] private string currentStateName;
    public IBossState currentState;
    public Boss_Idle _Idle = new Boss_Idle();
    public Boss_attack _Attack = new Boss_attack();
    public Boss_Summon _Summon = new Boss_Summon();
    public Boss_stun _stun = new Boss_stun();
    public Boss_death _death = new Boss_death();
    public Boss_shoot _Shoot = new Boss_shoot();
    private AimatPlayer aim;
    public RagDollEneable _DollEneable;

    public UnityEvent SummonEvent;
    [Header("AI Settings")]
    public float attackRange = 2f;

    [HideInInspector] public Animator anim;
    [HideInInspector] public Transform player;
    [HideInInspector] public Rigidbody rb;


    public int timecount = 0;
    private void Awake()
    {
     aim = GetComponent<AimatPlayer>();
        anim = GetComponentInChildren<Animator>();
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
    public void SwitchState(IBossState state)
    {
        if (currentState != null)
        {
            currentState.OnExitState(this);
        }
        currentState = state;
        state.OnEnterState(this);
        currentStateName = currentState.GetType().Name;
        Debug.Log("in state" + currentState);
    }
    
    private void OnDrawGizmos()
    {
    
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
