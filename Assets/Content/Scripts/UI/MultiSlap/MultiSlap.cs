using System;
using UnityEngine;
using UnityEngine.UI;

public class MultiSlap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image slider;
    public Button MultiSlapButton;


    event Action OnMultiSlapMeterFull;
    public static MultiSlap multiSlap_instance;
    private void OnEnable()
    {
        OnMultiSlapMeterFull += EnableMultiSlap;
    }
    private void OnDisable()
    {
        OnMultiSlapMeterFull -= EnableMultiSlap;
        
    }
    void Start()
    {
     

        if(multiSlap_instance == null)
        {
            multiSlap_instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.fillAmount >= 1)
        {

            OnMultiSlapMeterFull?.Invoke();
        }
    }
    public void ActivateMultiSlap()
    {
            SlapManager.slapManager_instance.bMultiSlap = true;

    }
    private void EnableMultiSlap()
    {
        MultiSlapButton.interactable = true;
    }
}
