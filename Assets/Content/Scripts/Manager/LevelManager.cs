using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static LevelManager Instance { get; private set; }
    [Space(20)]
    [Header("Game Over")]
    public GameObject GameOver_Screen_UI;
    public TextMeshPro Score_txt;
    public TextMeshPro Coin_txt;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart_lvl()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOver_Screen_UI.SetActive(true);
        Coin_txt.text = PlayerPrefs.GetFloat("coin").ToString();
    }
}
