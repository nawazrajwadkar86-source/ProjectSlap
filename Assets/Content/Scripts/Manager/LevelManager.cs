using System.Collections;
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
    public TextMeshProUGUI Score_txt;
    public TextMeshProUGUI Max_Score_txt;
    public TextMeshProUGUI Coin_txt;
    public TextMeshProUGUI Max_Coin_txt;
    [Space(20)]
    public GameObject Ad;
    public GameObject ad_Cut_btn;
    void Start()
    {
        Time.timeScale = 1;
        Instance = this;
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOver_Screen_UI.SetActive(true);
        Coin_txt.text = PlayerPrefs.GetInt("coin").ToString();
        Score_txt.text = Mathf.CeilToInt(Level_Difficulty.Instance.Distance_Travelled).ToString();

        Max_Score_txt.text = PlayerPrefs.GetFloat("max_distance_travelled").ToString();
        Max_Coin_txt.text = PlayerPrefs.GetInt("max_coin_score").ToString();
    }
    public void Show_ad()
    {
        Ad.SetActive(true);
        StartCoroutine(Ad_handler());
    }
    private IEnumerator Ad_handler()
    {
        yield return new WaitForSecondsRealtime(5);
        ad_Cut_btn.SetActive(true);
    }
    public void LoadLevel(string scene_name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene_name);
        
    }
}
