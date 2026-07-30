using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    public float HeatIncreaseValue = 5;
    [Range(0,1)]
    public float MultipleSlapValue = 0.2f;
    public event Action<ETargetType> onTargetHit;
    public Animator animator;
    private void OnEnable()
    {
        onTargetHit += ReceiveDamage;
        onTargetHit += UpdateScore;
        onTargetHit += UpdateHeatMeter;
        onTargetHit += UpdateMultiSlapMeter;
    }
    private void OnDisable()
    {
        
        onTargetHit -= ReceiveDamage;
        onTargetHit -= UpdateScore;
        onTargetHit -= UpdateHeatMeter;
        onTargetHit -= UpdateMultiSlapMeter;
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
    public void CallOnHitTargetEvent(ETargetType type)
    {
        Debug.Log("Event Called");
        onTargetHit?.Invoke(type);
    }
}
