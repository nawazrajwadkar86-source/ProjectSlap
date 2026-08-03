using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatMeter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static HeatMeter HeatMeter_Instance;


    public List<Image> Levels;
    public List<GameObject> Heat_multiplier;


    private void OnEnable()
    {

    }
    void Start()
    {

        Initialize_heatLevels();
        if(HeatMeter_Instance == null)
        {
            HeatMeter_Instance = this;
        }
    }
    private void Initialize_heatLevels()
    {
     foreach(var lvl in Levels)
        {
            lvl.fillAmount = 0;
        }
    }
    private void LateUpdate()
    {
       

    }

    public void Updateheat(float Heatval)
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            float remainig = 1 - Levels[i].fillAmount;

            if(remainig <= 0)
            {
               foreach(var heat in Heat_multiplier)
                {
                    heat.SetActive(false);
                }
               Heat_multiplier[i].SetActive(true);

                continue;
            }
            float amount = Mathf.Min(remainig, Heatval);
            Levels[i].fillAmount += amount;
            Heatval -= amount;
        }
    }
   
}
