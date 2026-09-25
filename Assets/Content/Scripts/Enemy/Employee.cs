using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Employee : Target
{    
    public float HeatIncreaseVal = 0.1f;
    public bool CanChase;
    private Vector3 targetLocation;
   
    public SO_Employee SO;
    protected override void Start()
    {
        this.HeatIncreaseValue = HeatIncreaseVal;
        player = GameObject.FindGameObjectWithTag("Player");
        baseSpeed = Speed;
        if(navAgent == null) navAgent = GetComponent<NavMeshAgent>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        if(navAgent == null)navAgent = GetComponent<NavMeshAgent>();
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
    protected override void ReceiveDamage(ETargetType type)
    {
       // bisSlapped = true;
        //animator.SetTrigger("hit");
    }
    protected override void UpdateMultiSlapMeter(ETargetType type)
    {
      //  MultiSlap.multiSlap_instance.slider.fillAmount += MultipleSlapValue;
    }
    protected override void UpdateHeatMeter(ETargetType type)
    {
        HeatMeter.HeatMeter_Instance.Updateheat(HeatIncreaseValue);
    }
    protected override void Reaction(ETargetType type)
    { 
        if(isChasing) return;
        if(chaseState != EChaseState.caught)
        {
            this.chaseState = EChaseState.chasing;
            isChasing = true;
            StartCoroutine(NpcEndLife());
        }
    }
    protected override void CaughtPlayer()
    {
        base.CaughtPlayer();
        Player_Health ph = FindAnyObjectByType<Player_Health>();
        if (ph)
        {
            ph.reduction_amount = 1;
            ph.Hurt();
            chaseState = EChaseState.caught;
        }
        else
        {
            Debug.LogError("no PH Found !");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && chaseState == EChaseState.chasing)
        {
            Debug.Log("player Found killed");
            CallOnCaughtPlayerEvent();
        }
    }

    public  override void SetSpeed(float new_speed)
    {
        Speed = new_speed;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(targetLocation, 0.2f);
    }
}
