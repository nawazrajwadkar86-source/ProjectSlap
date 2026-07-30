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
    public event Action<ETargetType> onTargetHit;
    Animator animator;
    private void OnEnable()
    {
        onTargetHit += ReceiveDamage;
        onTargetHit += UpdateScore;
        onTargetHit += UpdateHeatMeter;
    }
    private void OnDisable()
    {
        onTargetHit -= ReceiveDamage;
        onTargetHit -= UpdateScore;
        onTargetHit -= UpdateHeatMeter;
    }
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void ReceiveDamage(ETargetType type)
    {
        Debug.Log("Receive Damag");
    }

    protected virtual void UpdateScore(ETargetType type)
    {

        Debug.Log("update Score");
    }
    protected virtual void UpdateHeatMeter(ETargetType type)
    {
        HeatMeter.HeatMeter_Instance.Heat_val += HeatIncreaseValue;
        Debug.Log("update Score");
    }
    public void CallOnHitTargetEvent(ETargetType type)
    {
        Debug.Log("Event Called");
        onTargetHit?.Invoke(type);
    }
}
