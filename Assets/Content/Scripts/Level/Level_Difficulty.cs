using UnityEngine;

public class Level_Difficulty : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is
    
    public float Distance_Travelled;
    public float Max_Distance_Travelled;
    private Vector3 Init_location;
    public GameObject Player;
    public static Level_Difficulty Instance;

    void Start()
    {
     PlayerController pc = FindAnyObjectByType<PlayerController>();
        Init_location = Player.transform.position;
        Max_Distance_Travelled = PlayerPrefs.GetFloat("max_distance_travelled");
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
        if(Distance_Travelled > Max_Distance_Travelled)
        {
        Max_Distance_Travelled = Max_Distance_Travelled > Distance_Travelled ? Max_Distance_Travelled : Distance_Travelled;
        Max_Distance_Travelled = Mathf.CeilToInt(Max_Distance_Travelled);
        PlayerPrefs.SetFloat("max_distance_travelled", Max_Distance_Travelled);
        PlayerPrefs.Save();

        }

    }
}
