using System;
using UnityEditor.Animations;
using UnityEngine;

public class Obstacle_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public event Action OnObstacleHit;
    public static Obstacle_Manager Instance;
    public float Cached_Speed;
    public PlayerController pc;
    public Player_Health ph;
    public Animator Animator_;
    private void OnEnable()
    {
        OnObstacleHit += pc.Activate_Recharge;
        OnObstacleHit += ph.Hurt;
    }
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("obstacle"))
        {
   
            Cached_Speed = PlayerController.playerController_Instance.VerticalSpeed;
            PlayerController.playerController_Instance.VerticalSpeed /= 2;
            ph.reduction_amount = 0.5f;
            Animator_.SetTrigger("damage");

            OnObstacleHit?.Invoke();
        }
    }
}
