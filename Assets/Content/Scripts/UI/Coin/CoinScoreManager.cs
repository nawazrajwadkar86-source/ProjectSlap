using System;
using TMPro;
using UnityEngine;

public class CoinScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CoinScoreManager instance;
    public int CoinScore = 0;
    public int MaxCoinScore = 0;
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
        MaxCoinScore = PlayerPrefs.GetInt("max_coin_score");
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
        PlayerPrefs.SetInt("coin", CoinScore);

        if(CoinScore > MaxCoinScore)
        {
            MaxCoinScore = CoinScore;
            PlayerPrefs.SetInt("max_coin_score", CoinScore);
            PlayerPrefs.Save();
        }
    }
    public void EventOnCoinCollected()
    {
        OnCoinCollected?.Invoke();
    }
}
