using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    [System.Obsolete]

    public void PlayGame()
    {
        Application.LoadLevel("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RulesPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void Cancel()
    {
        settingsPanel.SetActive(false);
    }
}
