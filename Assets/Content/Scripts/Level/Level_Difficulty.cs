using UnityEngine;

public class Level_Difficulty : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is
    
    public float Distance_Travelled;
    private Vector3 Init_location;
    public GameObject Player;
    public static Level_Difficulty Instance;
    void Start()
    {
     PlayerController pc = FindAnyObjectByType<PlayerController>();
        Init_location = Player.transform.position;
    }
    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        DistanceBasedSpeed();
    }

    public void DistanceBasedSpeed()
    {
       Distance_Travelled = (Player.transform.position - Init_location).magnitude;


    }
}
