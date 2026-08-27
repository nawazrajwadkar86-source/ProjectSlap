using System;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator coin_animator;
    private void OnEnable()
    {
        coin_animator.Play("Coin_Rotation_anim");
    }

    void PlayCoinCollectedAnimation()
    {
        coin_animator.Play("Coin_collect");
        Invoke(nameof( DestroySelfReleaseMemeory), 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayCoinCollectedAnimation();
            CoinScoreManager.instance.EventOnCoinCollected();
        }
       
    }
    void DestroySelfReleaseMemeory()
    {

        gameObject.SetActive(false);
    }
}
