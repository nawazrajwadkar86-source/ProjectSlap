using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetReference", menuName = "Scriptable Objects/AssetReference")]
public class AssetReference : ScriptableObject
{
    public List<Material> worldCurveMaterial = new List<Material>();
}
