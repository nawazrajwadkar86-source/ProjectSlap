using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WorldPathCurve : MonoBehaviour
{
    public AssetReference assetReference;
    private bool rightSide = true;
    private float currentCurvedPath = 0.0015f;
    private float maxPathCurve = 0.0015f;
    public float CurveChangeSpeed = 0.0001f;

    private void Start()
    {
                    foreach(var m in assetReference.worldCurveMaterial)
            {
                m.SetFloat("_curve_Amount",0.0015f);
            }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ChangePath();
        }
    }
    private void ChangePath()
    {
        StartCoroutine(ChangeInPath());
    }
    IEnumerator ChangeInPath()
    {
        float time = 0;
        rightSide = !rightSide;
        int sign = rightSide? 1:-1;
        float cachedCurvedPath = currentCurvedPath;

        while(time < 1)
        {
            time += Time.deltaTime * CurveChangeSpeed;
            currentCurvedPath = Mathf.Lerp(cachedCurvedPath,sign * maxPathCurve,time);

            foreach(var m in assetReference.worldCurveMaterial)
            {
                m.SetFloat("_curve_Amount",currentCurvedPath);
            }
            yield return null;
        }
        
        yield return null;
    }
}
