using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject resumeButton;

    private void Start()
    {
        pauseMenu.SetActive(false);
        GameEvents.current.onPauseButtonClick += OnPauseMenuOpen;
        GameEvents.current.onResumeButtonClick += OnPauseMenuClose;
    }

    private void OnPauseMenuOpen()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        Time.timeScale = 0f;
        Player.instance.setIsPaused(true);
    }

    private void OnPauseMenuClose()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        Time.timeScale = 1f;
        Player.instance.setIsPaused(false);
    }

    private void OnDestroy()
    {
        GameEvents.current.onPauseButtonClick -= OnPauseMenuOpen;
        GameEvents.current.onResumeButtonClick -= OnPauseMenuClose;
    }
}
