using UnityEngine;

public class ActorGenerator : MonoBehaviour
{
    //Doors
    //NPC
    //Boosters
    //Traps

    private void Start()
    {
        spawnActors();
    }
    private void spawnActors()
    {
        
    }
    private bool GetRandomBool()
    {
        return Random.Range(0,2) == 1?true:false;
    }
}
