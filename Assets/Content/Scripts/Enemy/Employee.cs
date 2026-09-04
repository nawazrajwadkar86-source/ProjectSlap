using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Employee : Target
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody rb;
    public float HeatIncreaseVal = 0.2f;
    public bool CanChase;
    public float Speed;
    private float baseSpeed;
    private GameObject player;
    private Vector3 targetLocation;
    private Tween chaseT;
    public float Chase_Wait_Time;
    private float LifeTime = 12f;
    public enum EChaseState { 
    
        idle,
        chasing,
        caught

    }
    public EChaseState chaseState = EChaseState.idle;
    public SO_Employee SO;
    void Start()
    {
        this.HeatIncreaseValue = HeatIncreaseVal;
        player = GameObject.FindGameObjectWithTag("Player");
        //Speed = 1- SO.Speed;
        baseSpeed = Speed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        transform.localPosition = Vector3.zero;
        chaseState = EChaseState.idle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(chaseState == EChaseState.chasing)
        {
            Chase();
        }
        if(chaseState == EChaseState.caught)
        {
            rb.position = transform.position;
        }
    }
    protected override void ReceiveDamage(ETargetType type)
    {
       // bisSlapped = true;
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
        Debug.Log("Reaction Called");   

        if(chaseState != EChaseState.caught)
        {
        this.chaseState = EChaseState.chasing;
        StartCoroutine(NpcEndLife());
        }
    }
    protected override void SteeringSeparation()
    {
        if (chaseState == EChaseState.chasing)
        {
            Vector3 separation = Vector3.zero;

            Collider[] cols = Physics.OverlapSphere(transform.position, 1f);
            foreach (var col in cols)
            {
                if (player == null) return;
                if (col.gameObject == gameObject)
                {
                    continue;
                }
                if (col.transform.CompareTag("npc"))
                {
                    separation += (transform.position -
                        col.transform.position).normalized;
                }
            }
            Vector3 Desired = (player.transform.position - transform.position).normalized + separation * 2;
            targetLocation = transform.position + Desired;
        }
        else
        {
            return;
        }
    }
    private void Chase()
    {
        SteeringSeparation();
        if (player) {  
            targetLocation.y = 0.75f;

            if (Vector3.Distance(transform.position, player.transform.position) < 3f)
            {
                Speed = baseSpeed * 0.55f;
            }
            else
            {
                Speed = baseSpeed;
            }
            Vector3 targetPos = player.transform.position + player.transform.forward * -2.25f;
            rb.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.fixedDeltaTime);

            Invoke(nameof( WaitChase), Chase_Wait_Time);
        }  
    }
    private void WaitChase()
    {
        chaseT?.Kill();
        // chaseT = transform.DOMove(targetLocation, Speed).SetEase(Ease.Linear).OnComplete(Chase);
        //transform.position = ;
        
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
    IEnumerator NpcEndLife()
    {
        yield return new WaitForSeconds(LifeTime);
        transform.parent.transform.parent.gameObject.SetActive(false);
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
