using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Intern : Target
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float heat_val;
    private List<Target> nearbyTargets = new List<Target>();
    public float abilityEffectRange = 6;
    public LayerMask npcLayer;
    protected override void Start()
    {
        this.HeatIncreaseValue = heat_val;
        player = GameObject.FindGameObjectWithTag("Player");
        baseSpeed = Speed;
        if(navAgent == null)navAgent = GetComponent<NavMeshAgent>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        
        
        if(navAgent == null)navAgent = GetComponent<NavMeshAgent>();
        GetNearbyNpc();
        chaseState = EChaseState.idle;
        navAgent.Warp(Vector3.zero);
        transform.localPosition = Vector3.zero;
         isChasing = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(chaseState == EChaseState.chasing)
        {
            Chase();
            animator.SetBool("run",true);
        }
        if(chaseState == EChaseState.caught)
        {
            navAgent.SetDestination(transform.position);
            transform.position = transform.position;
            animator.SetBool("run",false);
        }
        if (chaseState == EChaseState.idle)
        {
            //if(navAgent.isActiveAndEnabled && navAgent.isOnNavMesh)navAgent.SetDestination(transform.position);
            animator.SetBool("run",false);
        }
    }
    protected override void Reaction(ETargetType type)
    {
        if(isChasing )return;

        if(chaseState != EChaseState.caught)
        {
             isChasing = true;
            this.chaseState = EChaseState.chasing;
            UseAbility();
            StartCoroutine(NpcEndLife());
        }
    }
    protected override void ReceiveDamage(ETargetType type)
    {
     //   bisSlapped = true;
        animator.SetTrigger("hit");
    }
    protected override void SteeringSeparation()
    {
    }
    protected override void UpdateHeatMeter(ETargetType type)
    {
        HeatMeter.HeatMeter_Instance.Updateheat(HeatIncreaseValue);
    }
    protected override void UpdateMultiSlapMeter(ETargetType type)
    {
    }
    protected override void UpdateScore(ETargetType type)
    {
    }
    private void UseAbility()
    {
        foreach(var n in nearbyTargets)
        {
            n.ChasePlayer();
        }
    }
    private void GetNearbyNpc()
    { 
        Collider [] col = Physics.OverlapSphere(transform.position,abilityEffectRange,npcLayer);

        foreach(var c in col)
        {
            nearbyTargets.Add(c.GetComponent<Target>());
        }
    }
    public override void ChasePlayer()
    {
        if(isChasing) return;
         if(chaseState != EChaseState.caught)
        {
             isChasing = true;
            this.chaseState = EChaseState.chasing;
            StartCoroutine(NpcEndLife());
        }
    }

    private void OnDrawGizmos()
    {
        //Ability range
        Gizmos.DrawWireSphere(transform.position,abilityEffectRange);
    }
}



