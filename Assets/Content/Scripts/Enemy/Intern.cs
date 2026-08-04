using UnityEngine;

public class Intern : Target
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float heat_val;
    void Start()
    {
       this.HeatIncreaseValue = heat_val;
    }

    // Update is called once per frame
    void Update()
    {

    }


    protected override void Reaction(ETargetType type)
    {

        
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
    
}



