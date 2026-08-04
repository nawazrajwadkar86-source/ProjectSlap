using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Employee : Target
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float HeatIncreaseVal = 0.05f;
    public bool CanChase;
    public float Speed;
    private GameObject player;
    private Vector3 targetLocation;
    private Tween chaseT;
    enum EChaseState { 
    
        idle,
        chasing

    }
    EChaseState chaseState = EChaseState.idle;
    void Start()
    {
        this.HeatIncreaseValue = HeatIncreaseVal;
        player = GameObject.FindGameObjectWithTag("Player");
     
    }

    // Update is called once per frame
    void Update()
    {
        if(chaseState == EChaseState.chasing)
        {
            Chase();
        }
    }
    protected override void ReceiveDamage(ETargetType type)
    {
        
        animator.SetTrigger("hit");
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
        Debug.Log($"{name} was slapped");
        this.chaseState = EChaseState.chasing;
   
    }
    protected override void SteeringSeparation()
    {
        Vector3 separation = Vector3.zero;

        Collider[] cols = Physics.OverlapSphere(transform.position, 1f);
        foreach(var col in cols)
        {
            if (player == null) return;
            if(col.gameObject == gameObject)
            {
                return;
            }
            if (col.transform.CompareTag("npc"))
            {
                separation += (transform.position - col.transform.position).normalized;
            }
            Vector3 Desired = (player.transform.position - transform.position).normalized + separation * 0.2f;
            targetLocation = transform.position + Desired;
        }
    }
    private void Chase()
    {
        Debug.Log($"{name} is chasing");
        if (player) {  
            targetLocation.y = 0.75f;
            SteeringSeparation();
            chaseT?.Kill(); 
            chaseT = transform.DOMove(targetLocation, Speed).SetEase(Ease.Linear).OnComplete(Chase);

        }
           // this.NMA.destination = targetLocation;
        

        
    }
}
