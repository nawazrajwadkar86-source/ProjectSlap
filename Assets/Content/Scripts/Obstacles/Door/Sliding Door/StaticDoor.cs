using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class StaticDoor : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();
    int openedDoorIndex;
    public UnityEvent ResetDamage;
    public void OnEnable()
    {
        openedDoorIndex = Random.Range(0,doors.Count);
        doors[openedDoorIndex].SetActive(false);

        ResetDamage?.Invoke();
    }
    public void OnDisable()
    {
        foreach(var g in doors)
        {
            g.SetActive(true);
        }

    }
}
