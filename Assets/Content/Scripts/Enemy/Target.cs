using System;
using UnityEngine;

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
    [Range(0,20)]
    public float HeatIncreaseValue = 0.2f;
    [Range(0,1)]
    public float MultipleSlapValue = 0.2f;
    public Animator animator;
    [HideInInspector] public bool bisSlapped;
   
    public event Action<ETargetType> onTargetHit;
    public event Action onCaughtPlayer;
    private void OnEnable()
    {
        onTargetHit += ReceiveDamage;
        onTargetHit += UpdateScore;
        onTargetHit += UpdateHeatMeter;
        onTargetHit += UpdateMultiSlapMeter;
        onTargetHit += Reaction;


        onCaughtPlayer += CaughtPlayer;
 
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
    void Start()
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
}
