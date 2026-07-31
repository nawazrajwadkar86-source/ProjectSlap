using UnityEngine;
using UnityEngine.UI;

public class HeatMeter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static HeatMeter HeatMeter_Instance;
    public float Heat_val;
    public float min_slider_value =0;
    public float max_slider_value =100;
    public Slider heat_slider;
    void Start()
    {
        heat_slider.minValue = min_slider_value;
        heat_slider.maxValue = max_slider_value;

        if(HeatMeter_Instance == null)
        {
            HeatMeter_Instance = this;
        }
    }

    private void LateUpdate()
    {
        heat_slider.value = Heat_val;

    }
}
