using System;
using TMPro;
using UnityEngine;

public class CoinScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CoinScoreManager instance;
    public int CoinScore = 0;
    public TextMeshProUGUI coinScoreTxt;
    public event Action OnCoinCollected;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        coinScoreTxt.text = $"{CoinScore}";
       
    }

    private void OnEnable()
    {
        OnCoinCollected += UpdateCoinScore;
    }
    private void OnDisable()
    {
        OnCoinCollected -= UpdateCoinScore;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateCoinScore()
    {
        CoinScore ++;
        coinScoreTxt.text =$"{CoinScore}";
    }
    public void EventOnCoinCollected()
    {
        OnCoinCollected?.Invoke();
    }
}
