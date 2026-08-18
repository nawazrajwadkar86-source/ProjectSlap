using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ActorsPlacementBluprint", menuName = "Scriptable Objects/Actors/ActorsPlacementBluprintList")]
public class ActorsPlacementBluprint : ScriptableObject
{
    public List<ActorPlacement> placementList = new List<ActorPlacement>();
}
