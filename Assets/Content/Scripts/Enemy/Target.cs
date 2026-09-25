using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

abstract public class Target : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum ETargetType
    {
        none,
        hr,
        manager,
        employee
    }
    public ETargetType type = ETargetType.employee;
    public EChaseState chaseState = EChaseState.idle;
    protected GameObject player;
    protected NavMeshAgent navAgent;
    protected float randomSteerDir;

    [Range(0,20)]
    public float HeatIncreaseValue = 0.5f;
    [Range(0,1)]
    public float MultipleSlapValue = 0.2f;
    public float Speed = 5;
    protected float baseSpeed;
    public float LifeTime = 8f;
    protected bool isChasing;
    public Animator animator;
    [HideInInspector] public bool bisSlapped;
   
    public event Action<ETargetType> onTargetHit;
    public event Action onCaughtPlayer;
    
    protected virtual void OnEnable()
    {

        onTargetHit += ReceiveDamage;
        onTargetHit += UpdateScore;
        onTargetHit += UpdateHeatMeter;
        onTargetHit += UpdateMultiSlapMeter;
        onTargetHit += Reaction;


        onCaughtPlayer += CaughtPlayer;
 
        randomSteerDir = UnityEngine.Random.Range(0.0f,1.0f) > 0.5f ? 1 : -1;
        
    }
    private void OnDisable()
    {
        onTargetHit -= ReceiveDamage;
        onTargetHit -= UpdateScore;
        onTargetHit -= UpdateHeatMeter;
        onTargetHit -= UpdateMultiSlapMeter;
        onTargetHit -= Reaction;

        onCaughtPlayer -= CaughtPlayer;
    }
    protected virtual void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    }
    protected virtual void ReceiveDamage(ETargetType type)
    {
        Debug.LogWarning("Receive Damage");
    }

    protected virtual void UpdateScore(ETargetType type)
    {
        Debug.Log("update Score");
    }
    protected virtual void UpdateHeatMeter(ETargetType type)
    {
        Debug.Log("update Score");
    }
    protected virtual void UpdateMultiSlapMeter(ETargetType type)
    {
        Debug.Log("update MultiSlap");
    }
    protected virtual void Reaction(ETargetType type)
    {
       Debug.Log("Reaction Called on Target");  
    }
    protected virtual void SteeringSeparation()
    {
    }
    public void CallOnHitTargetEvent(ETargetType type)
    {
        Debug.Log("Event Called");
        onTargetHit?.Invoke(type);
    }
    public void CallOnCaughtPlayerEvent()
    {
        onCaughtPlayer?.Invoke();
    }
    protected virtual void CaughtPlayer()
    {
    }
    public virtual void SetSpeed(float new_speed)
    {
    }
    public virtual void ChasePlayer()
    {
        
    }

    protected void Chase()
    {
        //SteeringSeparation();
        if (player) {  
            //targetLocation.y = 0.75f;

            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {
                Speed = baseSpeed * .6f;
            }
            else
            {
                Speed = baseSpeed;
            }
            Vector3 targetPos = player.transform.position + player.transform.forward * -2f + player.transform.right * randomSteerDir * UnityEngine.Random.Range(0.1f,0.4f);

            navAgent.speed = Speed;
            if (navAgent.isActiveAndEnabled && navAgent.isOnNavMesh)
            {
                navAgent.SetDestination(targetPos);
            }
        }  
    }
    public IEnumerator NpcEndLife()
    {
        yield return new WaitForSeconds(LifeTime);
        navAgent.ResetPath();
        navAgent.velocity = Vector3.zero;
        transform.parent.transform.parent.gameObject.SetActive(false);
    }
}
 public enum EChaseState { 
    
        idle,
        chasing,
        caught

    }