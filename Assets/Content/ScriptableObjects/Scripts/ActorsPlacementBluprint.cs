using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ActorsPlacementBluprint", menuName = "Scriptable Objects/Actors/ActorsPlacementBluprintList")]
public class ActorsPlacementBluprint : ScriptableObject
{
    public List<ActorPlacement> placementListSafeC = new List<ActorPlacement>();
    public List<ActorPlacement> placementListMixedC = new List<ActorPlacement>();
}
