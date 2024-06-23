using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panelPause;

    public void Play()
    {
        SceneManager.LoadScene(1); //("Game")
    }

    public void Exit()
    { 
        Application.Quit();
    }

    public void Pause()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }
}
