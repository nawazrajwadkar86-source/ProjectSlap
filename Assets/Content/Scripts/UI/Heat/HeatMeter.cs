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
    private float heatCooldownTime = 5;
    private float _time;
    public Slider heatCooldownSilder;
    private float totalHeatLevel = 3;
    private float currentHeatLevel;
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
    private void Update()
    {
        if(Levels[0].fillAmount <= 0)
        {
                heatCooldownSilder.gameObject.SetActive(false);
            return;
        }
                        if(!heatCooldownSilder.gameObject.activeSelf)heatCooldownSilder.gameObject.SetActive(true);

        _time += Time.deltaTime;
        if(_time < heatCooldownTime)
        {
            float heatCooldownValue = Mathf.Lerp(1,0,(Mathf.Round(_time))/5);
            heatCooldownSilder.value = heatCooldownValue;
        }

        if(_time >= heatCooldownTime)
        {
            Updateheat(-.2f);
            _time = 0;
        }

    }

    public void Updateheat(float Heatval)
    {
        if(Heatval >= 0.0f)
        {
            _time = 0;
        }

        currentHeatLevel += Heatval;
        currentHeatLevel = Mathf.Clamp(currentHeatLevel,0,totalHeatLevel);

        foreach(var heat in Heat_multiplier)
        {
            heat.SetActive(false);
        }

        for(int i = 0; i < totalHeatLevel; i++)
        {
            if(Mathf.CeilToInt(currentHeatLevel) - 1== i)
            {
                Heat_multiplier[i].SetActive(true);
            }


            if(Mathf.CeilToInt(currentHeatLevel) <= i + 1)
            {
                Levels[i].fillAmount = currentHeatLevel - (1 * i);
            }
        }


        // for (int i = 0; i < Levels.Count; i++)
        // {
        //     float remainig = 1 - Levels[i].fillAmount;

        //     
        //     if(remainig <= 0)
        //     {
        //        foreach(var heat in Heat_multiplier)
        //         {
        //             heat.SetActive(false);
        //         }
        //        Heat_multiplier[i].SetActive(true);

        //         continue;
        //     }


        //     float amount = Mathf.Min(remainig, Heatval);
        //     Levels[i].fillAmount += amount;
        //     Heatval -= amount;
        // }
    }
   
}
