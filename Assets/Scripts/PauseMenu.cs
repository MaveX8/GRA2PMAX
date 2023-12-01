using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void Resume()
    {
        Debug.Log("Resume game");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume time
    }

    public void Pause()
    {
        Debug.Log("Pause menu activated");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause time

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu"); // Replace "MainMenu" with your main menu scene name
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void TogglePauseMenu()
    {
        if (pauseMenuUI.activeSelf)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
