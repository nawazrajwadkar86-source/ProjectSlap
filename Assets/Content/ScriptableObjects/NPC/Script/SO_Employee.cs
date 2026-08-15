using UnityEngine;

[CreateAssetMenu(fileName = "SO_Employee", menuName = "Scriptable Objects/SO_Employee")]
public class SO_Employee : ScriptableObject
{
    [Range(0,1),Header("Speed of Employee in Rnage of 0-1")]
    public float Speed;
}
