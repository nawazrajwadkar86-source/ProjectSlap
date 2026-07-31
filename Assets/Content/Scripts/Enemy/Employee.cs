using UnityEngine;

public class Employee : Target
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void ReceiveDamage(ETargetType type)
    {

        animator.SetTrigger("hit");
    }
    protected override void UpdateMultiSlapMeter(ETargetType type)
    {
        MultiSlap.multiSlap_instance.slider.fillAmount += MultipleSlapValue;
    }
    protected override void UpdateHeatMeter(ETargetType type)
    {
        HeatMeter.HeatMeter_Instance.Heat_val += HeatIncreaseValue;
    
    }
}
