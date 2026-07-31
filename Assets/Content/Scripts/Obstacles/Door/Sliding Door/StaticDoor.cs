using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StaticDoor : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();
    int openedDoorIndex;
    public void Enable()
    {
        openedDoorIndex = Random.Range(0,doors.Count);
        doors[openedDoorIndex].SetActive(false);
    }
    public void OnDisable()
    {
        foreach(var g in doors)
        {
            g.SetActive(true);
        }
    }
}
