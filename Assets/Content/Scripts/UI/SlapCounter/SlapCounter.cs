using System;
using TMPro;
using UnityEngine;

public class SlapCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created\

    public TextMeshProUGUI slapCountTxt;
    public int SlapCount;

    public static SlapCounter Instance;
    void Start()
    {
        slapCountTxt.text = $"{SlapCount}";

        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EventOnSlap()
    {
        SlapCount++;
        slapCountTxt.text = $"{SlapCount}";
    }
}
