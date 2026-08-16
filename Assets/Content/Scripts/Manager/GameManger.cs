using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    private static GameManger _instance;
    public static GameManger instance
    {
        get{if(_instance != null)
            {
                return _instance;
            }
            else
            {
                GameObject g = new GameObject("GameManager");
                g.AddComponent<GameManger>();
                _instance = g.GetComponent<GameManger>();
                DontDestroyOnLoad(_instance);
                return _instance;
            }
        }
    }

    private void Awake()
    {
        if(_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
