using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuMng : MonoBehaviour
{
    public TextMeshProUGUI coin_T; 
    public TextMeshProUGUI gems_T; 

    private void Start()
    {
        UpdateUI();
        
    }
    private void UpdateUI()
    {
        coin_T.text = PlayerPrefs.GetInt("coin").ToString();
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Protoype");
    }
}
