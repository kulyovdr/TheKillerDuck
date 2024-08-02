using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject joystick;

    [SerializeField] Player player;

    private bool isAndroidControl = false;

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
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }


    public void Update()
    {
        Switch();
    }

    public void OnAndroidButtonClick()
    {
        isAndroidControl = true;
        joystick.SetActive(true);
    }

    public void OnPCButtonClick()
    {
        isAndroidControl = false;
        joystick.SetActive(false);
    }

    public int Switch()
    {
        if (isAndroidControl == true)
        {
            return 0;
        }
        else if (isAndroidControl == false)
        {
            return 1;
        }
        return 100; //пойми свойства дура и не пиши говно
    }
}
