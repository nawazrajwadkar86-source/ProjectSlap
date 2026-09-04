using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public void pauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
        pauseMenuUI.SetActive(value);
    }
}
