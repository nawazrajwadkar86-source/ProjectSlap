using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "ActorPlacement", menuName = "Scriptable Objects/Actors/ActorPlacement")]
public class ActorPlacement : ScriptableObject
{
    public PlacementRow[] PlacementSlots = new PlacementRow[6];
}
[System.Serializable]
public class PlacementRow
{
    public Category[] placmenColumns = new Category[3];
}
