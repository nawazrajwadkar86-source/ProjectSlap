using System.Collections.Generic;
using UnityEngine;

public class OnSpawnBehaviour : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();

    private void OnEnable()
    {
        foreach(var g in objects)
        {
            if (!g.activeSelf)
            {
                g.SetActive(true);
            }
        }
    }
}
