using System;
using System.Collections;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("obstacle"))
        {
            OnObstacleHit?.Invoke();

            other.GetComponent<IObstacle>().OnHit(other.ClosestPoint(transform.position)); 
            StartCoroutine(DamageEffect());
        }
    }
    private IEnumerator DamageEffect()
    {
        
        Animator_.applyRootMotion = true;
        Animator_.SetTrigger("damage");

        Cached_Speed = PlayerController.playerController_Instance.VerticalSpeed;
        PlayerController.playerController_Instance.VerticalSpeed = 0;

        yield return new WaitForSeconds(Animator_.GetCurrentAnimatorStateInfo(0).length);

        PlayerController.playerController_Instance.VerticalSpeed = Cached_Speed;
        ph.reduction_amount = 0.5f;
    }
}
