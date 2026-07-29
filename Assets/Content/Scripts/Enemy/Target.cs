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
    event Action onTargetHit;
    Animator animator;
    private void OnEnable()
    {
        onTargetHit += ReceiveDamage;
        onTargetHit += UpdateScore;
    }
    private void OnDisable()
    {
        onTargetHit -= ReceiveDamage;
        onTargetHit -= UpdateScore;
    }
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void ReceiveDamage()
    {

    }

    protected virtual void UpdateScore()
    {

    }

}
